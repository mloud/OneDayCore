using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

namespace OneDay.Core.Effects
{
    public class CloudsTransition : ABaseTransition
    {
        [SerializeField] Transform cloudContainer;
        [SerializeField] float distanceOffset = 2500;
        [SerializeField] float time = 1.0f;
        private Dictionary<Image, (Vector2 originalPos, Vector2 outOfScreenPos)> cloudsWithPosition;
        
        protected override void Awake()
        {
            cloudsWithPosition = new Dictionary<Image, (Vector2 originalPos, Vector2 outOfScreenPos)>();
            var clouds = cloudContainer.GetComponentsInChildren<Image>();
            foreach(var cloud in clouds)
            {
                cloudsWithPosition.Add(
                    cloud, 
                    (cloud.transform.localPosition, cloud.transform.localPosition + cloud.transform.localPosition.normalized * distanceOffset));
            }
        }

        protected override IEnumerator CustomShow()
        {
            var sequence = DOTween.Sequence();

            foreach(var cloud in cloudsWithPosition)
            {
                sequence.Join(cloud.Key.transform
                    .DOLocalMove(cloud.Value.originalPos, Random.Range(0.9f * time, 1.1f * time))
                    .From(cloud.Value.outOfScreenPos)
                    .SetEase(Ease.OutQuad));
            }

            yield return sequence.WaitForCompletion();
        }

        protected override IEnumerator CustomHide()
        {
            var sequence = DOTween.Sequence();
            foreach (var cloud in cloudsWithPosition)
            {
                sequence.Join(cloud.Key.transform
                    .DOLocalMove(cloud.Value.outOfScreenPos, Random.Range(0.9f * time, 1.1f * time))
                    .SetEase(Ease.InQuad));
            }

            yield return sequence.WaitForCompletion();
        }
    }
}
