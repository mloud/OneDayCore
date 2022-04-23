using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace OneDay.Core.Data
{
    public class LocalDataStorage : ABaseDataStorage
    {
        public override IEnumerator Save(string key, object data)
        {
            D.Info($"Saving key {key}");
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(data));
            yield break;
        }

        public override IEnumerator Load<T>(string key, Action<T> onFinished)
        {
            D.Info($"Loading key {key}");
            if (PlayerPrefs.HasKey(key))
            {
                var jsonValue = PlayerPrefs.GetString(key);
                onFinished(JsonConvert.DeserializeObject<T>(jsonValue));
            }
            else
            {
                D.Warning($"Key {key} not found");
                onFinished(default);
            }
            yield break;
        }
    }
}