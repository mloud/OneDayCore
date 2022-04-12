using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace OneDay.Core.Ui
{
    public class AudioManager : ABaseManager
    {
        [SerializeField] AudioSource MusicSource;

        private float maxMusicVolume;

        protected override void Awake()
        {
            base.Awake();
            maxMusicVolume = MusicSource.volume;
        }

        public void PlayMusic()
        {
            MusicSource.Play();
            StartCoroutine(ResumeMusic());
        }

        public IEnumerator PauseMusic()
        {
            yield return MusicSource.DOFade(0, 0.6f).WaitForCompletion();
        }

        public IEnumerator ResumeMusic()
        {
            yield return MusicSource.DOFade(maxMusicVolume, 0.6f).WaitForCompletion();
        }
    }
}