using UnityEngine;
using UnityEngine.Events;

namespace OneDay.Games.Jumper
{
    public class EndTrigger : MonoBehaviour
    {
        public UnityEvent OnPlayerEnter;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                OnPlayerEnter.Invoke();
            }
        }
    }
}