using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.Ui
{
    public class SceneUiImporter : ABaseMono
    {
        public List<Transform> Import(SceneUiContainer source, SceneUiContainer target)
        {
            var importedUi = new List<Transform>();
            if (source.PanelContainer != null)
            {
                if (target.PanelContainer != null)
                {
                    var panels = ImportPanels(source.PanelContainer, target.PanelContainer);
                    importedUi.AddRange(panels);
                }
                else
                {
                    Debug.LogError($"Target panel container in scene ui container {target.Name} not found");
                }
            }

            return importedUi;
        }

        private List<Transform> ImportPanels(Transform sourcePanelContainer, Transform targetPanelContainer)
        {
            var importedPanels = new List<Transform>();
            foreach(Transform panel in sourcePanelContainer)
            {
                panel.SetParent(targetPanelContainer);
                importedPanels.Add(panel);
            }
            return importedPanels;
        }
    }
}
