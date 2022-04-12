using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OneDay.Core.Effects
{
    public class TopDownStripesTransition : ABaseTransition
    {
        [SerializeField] Image topStripeImage;
        [SerializeField] Image bottomStripeImage;
        [SerializeField] float stripedDownDuration = 0.3f;
        protected override IEnumerator CustomShow()
        {
            var sequence = DOTween.Sequence();
            sequence.Join(topStripeImage.transform.DOScaleY(1, stripedDownDuration).From(0).SetEase(Ease.OutQuad));
            sequence.Join(bottomStripeImage.transform.DOScaleY(1, stripedDownDuration).From(0).SetEase(Ease.OutQuad));

            sequence.Append(label.DOFade(1, 0.5f).From(0));
            
            yield return sequence.WaitForCompletion();
        }

        protected override IEnumerator CustomHide()
        {
            var sequence = DOTween.Sequence();
            sequence.Join(label.DOFade(0, stripedDownDuration).From(0));
            sequence.Join(topStripeImage.transform.DOScaleY(0, stripedDownDuration).From(1).SetEase(Ease.OutQuad));
            sequence.Join(bottomStripeImage.transform.DOScaleY(0, stripedDownDuration).From(1).SetEase(Ease.OutQuad));

            yield return sequence.WaitForCompletion();
        }
    }
}
