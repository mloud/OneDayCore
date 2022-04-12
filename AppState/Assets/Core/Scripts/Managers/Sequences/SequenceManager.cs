using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Core.Sequences
{
    public class SequenceManager : ABaseManager
    {
        [SerializeField] List<ABaseSequence> sequences;

        ABaseSequence currentSequence;
        protected override void InternalInitialize()
        { }

        protected override void InternalRelease()
        { }

        public void PlaySequence(string sequenceId, KeyValueData data)
        {
            StartCoroutine(PlaySequenceAsync(sequenceId, data));
        }

        public IEnumerator PlaySequenceAsync(string sequenceId, KeyValueData data)
        {
            //Debug.Assert(currentSequence == null, $"There is already sequence playing {currentSequence.Id}");
            currentSequence = sequences.Find(x => x.Id == sequenceId);
            Debug.Assert(currentSequence != null, $"No such sequence with id {sequenceId} exists");
            yield return StartCoroutine(currentSequence.Play(data));
        }
    }
}