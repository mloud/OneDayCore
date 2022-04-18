using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class RotatorTween : ABaseTween
    {
        [SerializeField] Vector3 targetRotation;
        [SerializeField] Ease easeType = Ease.Linear;

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence.Append(transform
                .DOLocalRotate(targetRotation, time)
                .SetEase(easeType));
        }
    }
}