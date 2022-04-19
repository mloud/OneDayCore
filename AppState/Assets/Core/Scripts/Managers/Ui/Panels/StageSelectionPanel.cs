using System;
using System.Collections.Generic;
using OneDay.Core.Ui.Components;
using UnityEngine;

namespace OneDay.Core.Ui.Panels
{
    public class StageSelectionPanel : UiController
    {
        [SerializeField] private Transform stagesWidgetContainer;
        public class Model
        {
            public List<(bool finished, int stars, Action<int> onClicked)> Stages;
        }
        protected override void OnShow(KeyValueData data)
        {
            var model = data.Get<Model>("model");
            var stageWidgets = UiManager.Factory.CreateWidgets<StageWidget>(model.Stages.Count, stagesWidgetContainer);
            for (int i = 0; i < model.Stages.Count; i++)
            {
                stageWidgets[i].Set(new StageWidget.Model
                {
                    Level =  i + 1,
                    Stars =  model.Stages[i].stars,
                    Finished = model.Stages[i].finished
                }, model.Stages[i].onClicked);
            }
        }
    }
}