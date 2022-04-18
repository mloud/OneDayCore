using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OneDay.Core
{
    public abstract class PlayableMono : MonoBehaviour, IPlayable
    {
        public abstract void Play(Action onFinished, Object context = null);
        public abstract void Play(Object context = null);
    }
}
