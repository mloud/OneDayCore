using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OneDay.Core.Ui.Panels
{
    public class LoadingPanel : UiController
    {
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TMP_Text textLabel;

        public void Set(float progress01, string text)
        {
            progressSlider.value = progress01;
            textLabel.text = text;
        }
    }
}