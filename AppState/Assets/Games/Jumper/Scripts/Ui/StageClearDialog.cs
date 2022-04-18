using System;
using OneDay.Core;
using OneDay.Core.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.Games.Jumper.Ui
{
    public class StageClearDialog : UiController
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private Button claimButton;
  
        protected override void OnShow(KeyValueData data)
        {
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(()=>data.Get<Action>("claim").Invoke());
        }
    }
}
