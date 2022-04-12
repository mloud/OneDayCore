using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.Effects
{
    public class EffectManager : ABaseManager
    {
        [SerializeField] UiParticle xpParticlePrefab;
        [SerializeField] SmallUiParticle smallXpParticlePrefab;
        [SerializeField] GameObject carExplosionPrefab;

        [SerializeField] Canvas canvas;
        [SerializeField] RectTransform testDestinationTransform;

        private Dictionary<string, IEffectFactory> factories = new Dictionary<string, IEffectFactory>();

        protected override void InternalInitialize()
        {}

        protected override void InternalRelease()
        {}

        public void RegisterEffect(string id, IEffectFactory effectFactory)
        {
            factories.Add(id, effectFactory);
        }

        public void PlayEffect(string id, object parameters)
        {
            if (factories.TryGetValue(id, out var factory))
            {
                factory.Create(parameters);
            }
            else
            {
                Debug.LogError($"No factory for effect {id} found");
            }
        }

        public void PlayXpEffect(Vector3 fromWorldPosition, RectTransform destinationTransform)
        {
            var uiParticle = Instantiate(xpParticlePrefab, canvas.transform);
            uiParticle.transform.position = Camera.main.WorldToScreenPoint(fromWorldPosition);
            uiParticle.Play(destinationTransform, true);
        }

        public void PlaySmallXpUiParticle(Vector3 fromWorldPosition)
        {
            var uiParticle = Instantiate(smallXpParticlePrefab, canvas.transform);
            uiParticle.transform.position = Camera.main.WorldToScreenPoint(fromWorldPosition);
            uiParticle.Play(uiParticle.transform.position + new Vector3(0,70,0));
        }

        public void PlayCarExplosion(Vector3 position)
        {
            Instantiate(carExplosionPrefab, position, Quaternion.identity, transform);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //PlayXpEffect(
                //    Camera.main.ScreenToWorldPoint(Input.mousePosition), 
                //    testDestinationTransform ?? (RectTransform)canvas.transform);

                //PlaySmallXpUiParticle(
                //  Camera.main.ScreenToWorldPoint(Input.mousePosition));
             
            }
        }
    }
}