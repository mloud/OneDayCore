using System;
using UnityEngine;

namespace OneDay.Core
{
    public abstract class IPlayable : MonoBehaviour
    {
        public abstract void Play(Action onFinished);
        public abstract void Play();
    }
}
