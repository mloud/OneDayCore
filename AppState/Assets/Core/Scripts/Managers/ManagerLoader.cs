using UnityEngine;

namespace OneDay.Core
{
    public class ManagerLoader : ABaseMono
    {
        [SerializeField] ABaseManager Manager;
        [SerializeField] bool dontDestroy;

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(Manager.gameObject);
            ODApp.Instance.ManagerHub.Register(Manager);
        }
    }
}
