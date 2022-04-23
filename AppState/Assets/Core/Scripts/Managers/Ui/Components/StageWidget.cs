using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.Core.Ui.Components
{
    public class StageWidget : ABaseWidget
    {
        [SerializeField] private GameObject enabledBg;
        [SerializeField] private GameObject disabledBg;
        [SerializeField] private TMP_Text levelLabel;
        [SerializeField] private Transform starsContainer;
        [SerializeField] private Button button;
        [SerializeField] private GameObject shine;
       
        public struct Model
        {
            public int Level;
            public int Stars;
            public bool Finished;
            public bool Current;
        }

        private Model model;
        public void Set(Model model, Action<int> onClick)
        {
            this.model = model;
            levelLabel.text = (model.Level + 1).ToString();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(()=>onClick(this.model.Level));
            if (model.Stars != -1 && model.Finished)
            {
                starsContainer.gameObject.SetActive(true);
                for (int i = 0; i < starsContainer.childCount; i++)
                {
                    starsContainer.GetChild(i).GetChild(0).gameObject.SetActive(i < model.Stars);
                }
            }
            else
            {
                starsContainer.gameObject.SetActive(false);
            }
            shine.SetActive(model.Current);
            disabledBg.SetActive(!model.Finished && !model.Current);
            enabledBg.SetActive(model.Finished || model.Current);
            button.enabled = model.Current || model.Finished;
        }
    }
}