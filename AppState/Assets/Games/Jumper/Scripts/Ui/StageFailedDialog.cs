using System;
using System.Collections;
using OneDay.Core;
using OneDay.Core.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.Games.Jumper.Ui
{
    public class StageFailedDialog : UiController
    {
        [SerializeField] private Button confirmButton;
  
        protected override void OnShow(KeyValueData data)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(()=>StartCoroutine(OnConfirm(data.Get<Action>("confirm"))));
        }

        private IEnumerator OnConfirm(Action confirmAction)
        {
            confirmAction.Invoke();
            yield break;
        }
    }
}