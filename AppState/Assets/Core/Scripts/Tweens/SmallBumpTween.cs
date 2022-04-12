using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Tweens
{
    public class SmallBumpTween : ABaseTween
    {
        [SerializeField] Vector3 punch;

        protected override void CustomInternalPlay(Sequence sequence)
        {
            sequence.Append(transform
                .DOPunchScale(punch, time));
        }
    }
}
