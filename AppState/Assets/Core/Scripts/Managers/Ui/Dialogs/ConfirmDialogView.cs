using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.Core.Ui.Dialogs
{
    public class ConfirmDialogView : UiView
    {
        [SerializeField] TMP_Text label;
        public Button ConfirmButton;

        public void SetText(string text) => label.text = text;
    }
}
