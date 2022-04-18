using OneDay.Core.Ui;
using TMPro;
using UnityEngine;

namespace OneDay.Games.Jumper.Ui
{
    public class StagePanel : UiController
    {
        [SerializeField] private TMP_Text stageLabel;
   
        public void SetStage(int stage)
        {
            stageLabel.text = stage.ToString();
        }
    }
}