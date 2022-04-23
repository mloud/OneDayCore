using System;
using System.Collections;

namespace OneDay.Core.Data
{
    public abstract class ABaseDataStorage : ABaseMono
    {
        public abstract IEnumerator Save(string key, object data);
        public abstract IEnumerator Load<T>(string key, Action<T> onFinished);
    }
}