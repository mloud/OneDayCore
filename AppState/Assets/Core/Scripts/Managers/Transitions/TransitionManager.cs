using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.Effects
{
    public class TransitionManager : ABaseManager
    {
        [SerializeField] List<ABaseTransition> transitions;

        private ABaseTransition currentTransition;
      
        public IEnumerator Show(string id, string text = null, bool skipAnimation = false)
        {
            currentTransition = transitions.Find(x => x.Id == id);
            Debug.Assert(currentTransition, $"No such transition {id} found");
            currentTransition.gameObject.SetActive(true);
            yield return StartCoroutine(currentTransition.Show(text, skipAnimation));
        }

        public IEnumerator Hide()
        {
            //Debug.Assert(currentTransition != null, "No active transition");
            if (currentTransition != null)
            {
                yield return StartCoroutine(currentTransition.Hide());
                currentTransition.gameObject.SetActive(false);
            }
        }
    }
}