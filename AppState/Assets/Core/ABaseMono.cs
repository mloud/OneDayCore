using System;
using System.Collections;
using UnityEngine;

namespace OneDay.Core
{
    public class ABaseMono : MonoBehaviour
    {
        protected virtual void Awake()
        {}

        protected virtual void OnDestroy()
        {}

        public IEnumerator RunAfterCoroutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}

