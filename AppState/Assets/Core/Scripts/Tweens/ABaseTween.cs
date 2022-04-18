using UnityEngine;
using DG.Tweening;
using System;

namespace OneDay.Core.Tweens
{
    public abstract class ABaseTween : IPlayable
    {
        [SerializeField] private string id;
        [SerializeField] private bool noOtherTweenPlaying;
        [SerializeField] private bool playOnEnable;
        [SerializeField] private float delay;
        [SerializeField] protected float time = 0.3f;
        [SerializeField] private int loop = 1;
        [SerializeField] private LoopType loopType = LoopType.Incremental;

        private Sequence sequence;
        public override void Play()
        {
            if (!noOtherTweenPlaying || !DOTween.IsTweening(gameObject))
            {
                InternalPlay(null);
            }
        }

        public override void Play(Action onFinished = null)
        {
            if (!noOtherTweenPlaying || !DOTween.IsTweening(gameObject))
            {
                InternalPlay(onFinished);
            }
        }

        protected abstract void CustomInternalPlay(Sequence sequence);

        private void InternalPlay(Action onFinished)
        {
            sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            CustomInternalPlay(sequence);
            sequence.onComplete += () =>
            {
                sequence = null;
                onFinished?.Invoke();
            };
            sequence.SetLoops(loop, loopType);
        }

        private void OnEnable()
        {
            if (playOnEnable)
            {
                InternalPlay(null);
            }
        }

        private void OnDestroy()
        {
            sequence?.Kill();
        }
    }
}
