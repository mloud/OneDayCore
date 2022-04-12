using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace OneDay.Core.Ui.Components
{
    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] UnityEvent<float> TickEvent;

        [SerializeField] TMP_Text timeLabel;
    
        private float timer;
        private Coroutine updateCoroutine;
        private WaitForSeconds wait;

        private void Awake()
        {
            wait = new WaitForSeconds(1.0f);
            gameObject.SetActive(false);
        }

        public void StartCountDown(float seconds)
        {
            gameObject.SetActive(true);
            timer = seconds;
            if (updateCoroutine != null)
            {
                StopCoroutine(updateCoroutine);
            }
            updateCoroutine = StartCoroutine(UpdateCoroutine());
        }
        
        public void Cancel()
        {
            if (updateCoroutine != null)
            {
                StopCoroutine(updateCoroutine);
                updateCoroutine = null;
            }
            gameObject.SetActive(false);
        }

        private IEnumerator UpdateCoroutine()
        {
            while (timer > 0)
            {
                timeLabel.text = ((int)timer).ToString();
                TickEvent?.Invoke(timer);
                yield return wait;
                timer = Mathf.Max(0, timer - 1);
            }
            gameObject.SetActive(false);
        }
    }
}
