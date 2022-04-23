using System;
using System.Collections.Generic;
using OneDay.Core.GeneralModules;
using OneDay.Core.Ui.Components;
using UnityEngine;

namespace OneDay.Core.Ui.Panels
{
    public class StageSelectionPanel : UiController
    {
        [SerializeField] private Transform stagesWidgetContainer;
        public class Model
        {
            public List<(bool finished, int stars, bool current, Action<int> onClicked)> Stages = new();

            public static Model From(HyperCasualProgressionModule progressionModule, Action<int> onStageClicked)
            {
                var model = new Model();
                
                for (int i = 0; i < progressionModule.LevelsCount(); i++)
                {
                    model.Stages.Add((
                        progressionModule.IsLevelFinished(i), 
                        progressionModule.GetLevelStars(i),
                        i == progressionModule.GetLevel(),
                        onStageClicked));        
                }

                return model;
            }
        }
        protected override void OnShow(KeyValueData data)
        {
            var model = data.Get<Model>("model");
            var stageWidgets = UiManager.Factory.CreateWidgets<StageWidget>(model.Stages.Count, stagesWidgetContainer);
            for (int i = 0; i < model.Stages.Count; i++)
            {
                stageWidgets[i].Set(new StageWidget.Model
                {
                    Level =  i,
                    Stars =  model.Stages[i].stars,
                    Finished = model.Stages[i].finished,
                    Current = model.Stages[i].current
                }, model.Stages[i].onClicked);
            }
        }
    }
}