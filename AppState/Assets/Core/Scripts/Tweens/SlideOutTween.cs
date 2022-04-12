using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class SlideOutTween : ABaseTween
    {
        [SerializeField] Ease easeType = Ease.Linear;
        [SerializeField] Vector3 toOffset;

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence.Append(transform
                .DOMove(transform.position + toOffset, time)
                .SetEase(easeType));
        }
    }
}
