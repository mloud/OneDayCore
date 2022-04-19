using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OneDay.Core;
using OneDay.Core.States;
using OneDay.Core.Ui;
using OneDay.Core.Ui.Panels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDay.Games.Jumper
{
    public class JumperStateCallbacks : ABaseMono
    {
        public IEnumerator EnterBoot()
        {
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
        public IEnumerator EnterMenu()
        {
            Debug.Log("EnterMenu");

            void OnStageClicked(int index)
            {
                ODApp.Instance.ManagerHub.Get<StateManager>().Trigger("StartGame");
            }
            
            var levelSelectionModel = new StageSelectionPanel.Model();
            levelSelectionModel.Stages = new List<(bool finished, int stars, Action<int>)>
            {
                (false, -1,OnStageClicked),
                (false, -1,OnStageClicked),
                (false, -1,OnStageClicked),
                (false, -1,OnStageClicked),
                (false, -1,OnStageClicked),
                (false, -1,OnStageClicked),
                (false, -1,OnStageClicked),
                (false, -1,OnStageClicked)
            };
            yield return ODApp.Instance.ManagerHub.Get<UiManager>().Show("LevelSelectionPanel",
                KeyValueData.Create().Add("model", levelSelectionModel));
            
            yield break;
        }

        public IEnumerator LeaveMenu()
        {
            Debug.Log("LeaveMenu");
            yield break;
        }
        
        public IEnumerator EnterGame()
        {
            Debug.Log("EnterGame");
            yield break;
        }

        public IEnumerator LeaveGame()
        {
            Debug.Log("LeaveGame");
            yield break;
        }
    }
}