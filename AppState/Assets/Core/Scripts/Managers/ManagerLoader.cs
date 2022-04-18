using System;
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

            if (dontDestroy)
            {
                DontDestroyOnLoad(Manager.gameObject);
            }

            ODApp.Instance.ManagerHub.Register(Manager);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (!dontDestroy)
            {
                ODApp.Instance.ManagerHub.Unregister(Manager);     
            }
        }
    }
}
