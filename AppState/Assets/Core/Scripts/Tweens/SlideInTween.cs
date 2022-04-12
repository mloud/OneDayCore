using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class SlideInTween : ABaseTween
    {
        [SerializeField] Ease easeType = Ease.Linear;
        [SerializeField] Vector3 fromOffset;

        private Vector3 originalPosition;
        private void Awake()
        {
            originalPosition = transform.localPosition;
        }

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence.Append(transform
                .DOLocalMove(originalPosition, time)
                .From(originalPosition + fromOffset)
                .SetEase(easeType));
        }
    }
}
