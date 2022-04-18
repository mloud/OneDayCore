using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngineInternal;

namespace OneDay.Games.Jumper
{
    public class CatchTrigger : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        public float MaxSpeed;
        public float Speed;

        private bool killed;
        public IEnumerator Vanish()
        {
            killed = true;
            var seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => Speed, (v) => Speed = v, 0, 0.4f));
            seq.AppendInterval(0.8f);
            seq.AppendCallback(() => animator.SetTrigger("Die"));
            seq.AppendInterval(2.0f);
            seq.Append(transform.DOMove(transform.position + new Vector3(0,-1.0f,0), 0.6f).SetEase(Ease.Linear));

            yield return seq.WaitForCompletion();
        }
        
        
        private void Update()
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
            animator.SetFloat("Speed", Speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (killed)
                return;
            
            if (other.name == "Player")
            {
                DOTween.To(() => Speed, (v) => Speed = v, 0, 0.3f);
                animator.SetTrigger("Attack");
                other.GetComponent<SimpleCharacter>().Kill();
            }
        }
    }
}