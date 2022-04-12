using UnityEngine;
using UnityEngine.Events;

namespace OneDay.Core
{
    [DefaultExecutionOrder(-100)]
    public abstract class AGameStartup : ABaseMono
    {
        [SerializeField] UnityEvent GameStartupCompleted;
        protected override void Awake()
        {
            base.Awake();
            Setup();
        }

        protected void Start()
        {
            GameStartupCompleted.Invoke();
        }

        protected abstract void Setup();
    }
}
