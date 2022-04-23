using System;
using System.Collections;
using Core.Modules;
using DG.Tweening;
using OneDay.Core;
using OneDay.Core.Data;
using OneDay.Core.GeneralModules;
using OneDay.Core.Modules;
using OneDay.Core.States;
using OneDay.Core.Ui;
using OneDay.Core.Ui.Panels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDay.Games.Jumper
{
    public class JumperStateCallbacks : ABaseMono
    {
        [SerializeField] private ProgressionSettings progressionWorldData; 
        public IEnumerator EnterBoot(KeyValueData data)
        {
            var dataManager = ODApp.Instance.ManagerHub.Get<DataManager>();

            
            // world
            IHyperCasualWorldData wordProgressionData = progressionWorldData;
            //user

            // progression
            const string ProgressionKey = "Progression";
            HyperCasualProgressionData userProgressionData = null;
            yield return ODApp.Instance.ManagerHub.Get<DataManager>().Load<HyperCasualProgressionData>(ProgressionKey, (data)=> userProgressionData = data);
            userProgressionData.Save += () => dataManager.Save(ProgressionKey, userProgressionData);
            
            
            // configure Modules here
            var userModuleRegister = new ModuleRegister();
            userModuleRegister.Register<HyperCasualProgressionModule>(
                new HyperCasualProgressionModule(userProgressionData, wordProgressionData));
            ODApp.Instance.ModuleHub.Register("user", userModuleRegister);
            
            
            
            D.Info($"EnterBoot on scene {SceneManager.GetActiveScene().name}");
            var loadingPanel = ODApp.Instance.ManagerHub.Get<UiManager>().Get<LoadingPanel>("LoadingPanel");
            float value = 0;
            var loadingTween = DOTween.To(
                () => value,
                (v) => value = v,
                1,
                2.0f);

            loadingTween.onUpdate += () =>
                loadingPanel.Set(value, $"Loading... {Math.Ceiling(value * 100).ToString()}%");

            yield return loadingTween.WaitForCompletion();
        }

        public IEnumerator LeaveBoot()
        {
            D.Info("LeaveConfig");
            yield break;
        }
        public IEnumerator EnterMenu(KeyValueData data)
        {
            Debug.Log("EnterMenu");

            void OnStageClicked(int index)
            {
                ODApp.Instance.ManagerHub.Get<StateManager>()
                    .Trigger("StartGame", KeyValueData.Create().Add("level", index));
            }

            var progressionModule = ODApp.Instance.ModuleHub.Get("user").Get<HyperCasualProgressionModule>();
            var levelSelectionModel = StageSelectionPanel.Model.From(progressionModule, OnStageClicked);
           
            yield return ODApp.Instance.ManagerHub.Get<UiManager>().Show("LevelSelectionPanel",
                KeyValueData.Create().Add("model", levelSelectionModel));
        }

        public IEnumerator LeaveMenu()
        {
            Debug.Log("LeaveMenu");
            yield break;
        }
        
        public IEnumerator EnterGame(KeyValueData data)
        {
            Debug.Log("EnterGame");
            ODApp.Instance.ManagerHub.Get<JumperGameManager>().StartLevel(data.Get<int>("level"));
            yield break;
        }

        public IEnumerator LeaveGame()
        {
            Debug.Log("LeaveGame");
            yield break;
        }
    }
}