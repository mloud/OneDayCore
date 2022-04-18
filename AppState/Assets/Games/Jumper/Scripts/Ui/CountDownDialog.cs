using System.Collections;
using OneDay.Core;
using OneDay.Core.Ui;
using TMPro;
using UnityEngine;

namespace OneDay.Games.Jumper.Ui
{
    public class CountDownDialog : UiController
    {
        [SerializeField] private TMP_Text label;
  
        protected override void OnShow(KeyValueData data)
        {
            StartCoroutine(StartCountDownEnumerator(
                data.Get<int>("from"),
                data.Get<float>("delay")));
        }

        private IEnumerator StartCountDownEnumerator(int fromValue, float delay)
        {
            int counter = fromValue;
            while (counter >= 0)
            {
                label.text = counter.ToString();
                counter--;
                yield return new WaitForSeconds(delay);
            }

            StartCoroutine(Hide());
        }
    }
}