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

            if (source.DialogContainer != null)
            {
                if (target.DialogContainer != null)
                {
                    var dialogs = ImportDialogs(source.DialogContainer, target.DialogContainer);
                    importedUi.AddRange(dialogs);
                }
                else
                {
                    Debug.LogError($"Target dialog container in scene ui container {target.Name} not found");
                }
            }
           
            return importedUi;
        }

        private List<Transform> ImportPanels(Transform sourcePanelContainer, Transform targetPanelContainer)
        {
            var importedPanels = new List<Transform>();
            foreach(Transform panel in sourcePanelContainer)
            {
                importedPanels.Add(panel);
            }
            foreach (var importedPanel in importedPanels)
            {
                importedPanel.SetParent(targetPanelContainer);
            }
            return importedPanels;
        }
        
        private List<Transform> ImportDialogs(Transform sourceDialogContainer, Transform targetDialogContainer)
        {
            var importedDialogs = new List<Transform>();
            foreach(Transform panel in sourceDialogContainer)
            {
                importedDialogs.Add(panel);
            }
            
            foreach (var importedDialog in importedDialogs)
            {
                importedDialog.SetParent(targetDialogContainer);
            }
            return importedDialogs;
        }
    }
}
