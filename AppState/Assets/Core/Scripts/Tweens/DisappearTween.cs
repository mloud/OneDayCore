using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class DisappearTween : ABaseTween
    {
        [SerializeField] string Id;
        [SerializeField] float heightOffset;
        [SerializeField] Ease easeType = Ease.Linear;

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence.Append(transform
                .DOMoveY(transform.position.y + heightOffset, time)
                .SetEase(easeType));
        }
    }
}
