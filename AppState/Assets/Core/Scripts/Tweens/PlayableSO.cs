using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OneDay.Core.Tweens
{
    [CreateAssetMenu(fileName = "PlayableSO", menuName = "OneDay/Tweens/PlayableSO", order = 1)] 
    public class PlayableSO : ScriptableObject, IPlayable
    {
        public TweenConfig Config;

        public void Play(Object context = null)
        {
            TweenRunner.Run(Config, null, context);
        }
        
        public void Play(Action onFinished, Object context = null)
        {
            TweenRunner.Run(Config, onFinished, context);
        }
    }
}