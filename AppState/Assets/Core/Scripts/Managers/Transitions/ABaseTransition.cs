using OneDay.Core.PropertyDrawers;
using System.Collections;
using TMPro;
using UnityEngine;

namespace OneDay.Core.Effects
{
    public abstract class ABaseTransition : ABaseMono
    {
        public string Id;
        [SerializeField] protected TMP_Text label;

        [InspectorButton("OnClickShow")]
        public bool clickShow;
        [InspectorButton("OnClickHide")]
        public bool clickHide;

        public IEnumerator Show(string text, bool skipAnimation)
        {
            label.text = text;
            if (!skipAnimation)
            {
                yield return StartCoroutine(CustomShow());
            }
        }

        public IEnumerator Hide()
        {
            yield return StartCoroutine(CustomHide());
        }

        protected abstract IEnumerator CustomShow();
        protected abstract IEnumerator CustomHide();


        private void OnClickShow()
        {
            gameObject.SetActive(true);
            StartCoroutine(InspectorClickShowCoroutine());
        }

        private void OnClickHide()
        {
            StartCoroutine(InspectorClickHideCoroutine());
        }

        private IEnumerator InspectorClickShowCoroutine()
        {
            yield return StartCoroutine(Show("text", false));
        }

        private IEnumerator InspectorClickHideCoroutine()
        {
            yield return StartCoroutine(Hide());
            gameObject.SetActive(false);
        }

    }
}