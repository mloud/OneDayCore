using DG.Tweening;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    public class EndTriggerGateAnim : MonoBehaviour
    {
        [SerializeField] private Transform leftDoor;
        [SerializeField] private Transform rightDoor;

        public void Play()
        {
            rightDoor.DOLocalRotate(Vector3.zero, 0.2f);
            leftDoor.DOLocalRotate(Vector3.zero, 0.2f);
        }
    }
}