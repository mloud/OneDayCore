using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OneDay.Core.Effects
{
    public class FadeTransition : ABaseTransition
    {
        [SerializeField] Image image;
        [SerializeField] float duration = 0.3f;
        protected override IEnumerator CustomShow()
        {
            var sequence = DOTween.Sequence();
            image.enabled = true;
            sequence.Join(image.DOFade(1.0f, duration));
            yield return sequence.WaitForCompletion();
        }

        protected override IEnumerator CustomHide()
        {
            var sequence = DOTween.Sequence();
            sequence.Join(image.DOFade(0.0f, duration));
            yield return sequence.WaitForCompletion();
            image.enabled = false;
        }
    }
}
