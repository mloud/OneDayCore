using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class ScaleTween : ABaseTween
    {
        [SerializeField] string Id;
        [SerializeField] Vector3 fromScale;
        [SerializeField] Vector3 toScale;
        [SerializeField] Ease easeType = Ease.Linear;

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence.Append(transform
                .DOScale(toScale, time)
                .From(fromScale)
                .SetEase(easeType));
        }
    }
}
