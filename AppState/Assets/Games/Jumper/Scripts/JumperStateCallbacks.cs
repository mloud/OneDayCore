using System;
using System.Collections;
using DG.Tweening;
using OneDay.Core;
using OneDay.Core.Ui;
using OneDay.Core.Ui.Panels;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    public class JumperStateCallbacks : ABaseMono
    {
        public IEnumerator EnterBoot()
        {
            D.Info("EnterBoot");
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