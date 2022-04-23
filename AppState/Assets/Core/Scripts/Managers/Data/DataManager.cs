using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace OneDay.Core.Data
{
    public class DataManager : ABaseManager
    {
        [Serializable]
        public class KeyToStorage
        {
            public string Key;
            public ABaseDataStorage Storage;
        }

        [SerializeField] private List<KeyToStorage> keyStorages;

        protected override void InternalInitialize()
        {
        }

        protected override void InternalRelease()
        {  }

        public void Save(string key, object data)
        {
            StartCoroutine(SaveCoroutine(key, data));
        }
        
        public IEnumerator SaveCoroutine(string key, object data)
        {
            D.Info($"Saving key {key}");

            var keyToStorage = keyStorages.Find(x => x.Key == key);
            if (keyToStorage != null)
            {
                yield return keyToStorage.Storage.Save(key, data);
            }
            else
            {
                D.Error($"Could not save key {key}, storage not found");   
            }
        }

        public IEnumerator Load<T>(string key, Action<T> onFinished) where T:new()
        {
            D.Info($"Loading key {key}");
            var keyToStorage = keyStorages.Find(x => x.Key == key);
            if (keyToStorage != null)
            {
                yield return keyToStorage.Storage.Load<T>(key, (data)=> onFinished(data ?? new T()));
            }
            else
            {
                D.Error($"Could not load key {key}, storage not found");
                onFinished(default);
            }
        }
    }
}