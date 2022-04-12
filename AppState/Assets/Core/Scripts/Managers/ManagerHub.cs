using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core
{
    public class ManagerHub : ABaseMono
    {
        [SerializeField] List<ABaseManager> managers;    

        public T Get<T>() where T: ABaseManager
        {
            var manager = managers.Find(x => x.GetType() == typeof(T));
            if (manager == null)
            {
                Debug.LogError($"No such manager exists {typeof(T)}");
            }
            return (T)manager;
        }
        
        public ABaseManager Get(Type type) => managers.Find(x => x.GetType() == type);
        

        public void Register(ABaseManager manager)
        {
            Debug.Assert(Get(manager.GetType()) == null, $"Manager with type {manager.GetType()} is already registered");
            managers.Add(manager);
            manager.Initialize();
        }
    }
}
