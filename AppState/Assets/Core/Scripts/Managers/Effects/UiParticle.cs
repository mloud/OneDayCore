using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Effects
{
    public class UiParticle : MonoBehaviour
    {
        public void Play(RectTransform destination, bool bounce)
        {
            transform.DOMove(destination.position, 1).SetEase(Ease.InSine).onComplete += ()=>OnComplete(destination,bounce);
            transform.DOScale(1.0f, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
        }

        private void OnComplete(RectTransform destination, bool bounce)
        {
            if (bounce)
            {
                destination.DOKill(true);
                destination.DOPunchScale(new Vector3(0.2f, 0.2f, 1.0f), 0.15f, 10, 1f);
            }
            Destroy(gameObject);
        }
    }   
}