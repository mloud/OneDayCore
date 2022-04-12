using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Effects
{
    public class SmallUiParticle : MonoBehaviour
    {
        public void Play(Vector3 destinationPosition)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(destinationPosition, 1).SetEase(Ease.InSine));
            sequence.Join(transform.DOScale(1.0f, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack));
            sequence.onComplete += OnComplete;

            //transform.DOMove(destinationPosition, 1).SetEase(Ease.InSine).onComplete += OnComplete;
            //transform.DOScale(1.0f, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
        }

        private void OnComplete()
        {
            Destroy(gameObject);
        }
    }
}