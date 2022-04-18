using System;
using Object = UnityEngine.Object;

namespace OneDay.Core
{
    public interface IPlayable
    {
        void Play(Object context = null);
        void Play(Action onFinished, Object context = null);
    }
}