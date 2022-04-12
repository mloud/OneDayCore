using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class CloudTween : ABaseTween
    {
        [SerializeField] Ease easeType = Ease.Linear;
        [SerializeField] Vector3 offset;

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence
                .Append(transform.DOLocalMove(transform.position + offset, time).SetEase(easeType))
                .Append(transform.DOLocalMove(transform.position - offset, time).SetEase(easeType))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
