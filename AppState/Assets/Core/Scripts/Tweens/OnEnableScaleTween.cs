using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class OnEnableScaleTween : ABaseTween
    {
        [SerializeField] Ease easeType = Ease.Linear;
        [SerializeField] Vector3 fromScale;
        [SerializeField] Vector3 toScale;

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence.Append(transform
                .DOScale(toScale, time)
                .SetEase(easeType)
                .From(fromScale));
        }
    }
}
