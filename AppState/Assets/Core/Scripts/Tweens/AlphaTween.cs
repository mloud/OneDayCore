using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace OneDay.Core.Tweens
{
    public class AlphaTween : ABaseTween
    {
        [SerializeField] Ease easeType = Ease.Linear;
        [SerializeField] float fromAlpha;
        [SerializeField] float toAlpha;
        [Space]
        [Header("Targets")]
        [SerializeField] Image image;
        [SerializeField] TMP_Text label;
        [SerializeField] SpriteRenderer spriteRen;
        [SerializeField] CanvasGroup canvasGroup;
        protected override void CustomInternalPlay(Sequence sequence)
        {

            if (image != null)
            {
                sequence.Join(image
                    .DOFade(toAlpha, time)
                    .From(fromAlpha)
                    .SetEase(easeType));

            }
            if (spriteRen != null)
            {
                sequence.Join(spriteRen
                    .DOFade(toAlpha, time)
                    .From(fromAlpha)
                    .SetEase(easeType));
            }

            if (label != null)
            {
                sequence.Join(label
                    .DOFade(toAlpha, time)
                    .From(fromAlpha)
                    .SetEase(easeType));
            }

            if (canvasGroup != null)
            {
                sequence.Join(canvasGroup
                    .DOFade(toAlpha, time)
                    .From(fromAlpha)
                    .SetEase(easeType));
            }

        }
    }
}
