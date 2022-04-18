using System;
using System.Collections;
using UnityEngine;

namespace OneDay.Core.Ui
{
    public abstract class UiController : UiController<NoUiView>
    { }

    public abstract class UiController<T> : InjectableMono, IShowable where T: UiView
    {
        [SerializeField] bool showByDefault;
        [SerializeField] IPlayable showBehaviour;
        [SerializeField] IPlayable hideBehaviour;
        [SerializeField] GameObject root;
        protected T View { get; private set; }

        private Action onHideAction;

        protected override void Awake()
        {
            base.Awake();

            View = (T)GetComponent<UiView>();
            if (showByDefault)
            {
                StartCoroutine(Show(null));
            }
            else
            {
                root.SetActive(false);
            }
        }

        public IEnumerator Hide()
        {
            OnHide();

            if (hideBehaviour != null)
            {
                bool finished = false;
                hideBehaviour.Play(() =>
                {
                    finished = true;
                    root.SetActive(false);
                });
                yield return new WaitUntil(() => finished);
            }
            else
            {
                gameObject.SetActive(false);
            }
            onHideAction?.Invoke();
            onHideAction = null;
        }

        public IEnumerator Show(KeyValueData data = null, Action onHide = null)
        {
            onHideAction = onHide;
            OnShow(data);

            if (showBehaviour != null)
            {
                bool finished = false;
                root.SetActive(true);
                showBehaviour.Play(() => finished = true);
                yield return new WaitUntil(() => finished);
            }
            else
            {
                root.SetActive(true);
            }
        }

        protected virtual void OnShow(KeyValueData data) { }
        protected virtual void OnHide() { }
    }
}
