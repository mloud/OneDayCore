using System;
using Object = UnityEngine.Object;

namespace OneDay.Core.Tweens
{
    public class GeneralTween : PlayableMono
    {
        public TweenConfig Config;
        public bool PlayOnEnable;

        private void OnEnable()
        {
            if (PlayOnEnable)
            {
                Play();
            }
        }

        public override void Play(Action onFinished, Object context = null)
        {
            TweenRunner.Run(Config, onFinished, context);
        }

        public override void Play(Object context = null)
        {
            TweenRunner.Run(Config, null, context);
        }
    }
}