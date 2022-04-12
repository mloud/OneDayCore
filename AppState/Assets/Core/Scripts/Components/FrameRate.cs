using UnityEngine;

namespace OneDay.Core.Components
{
    public class FrameRate : MonoBehaviour
    {
        [SerializeField] int targetFrameRate;
        private void Awake()
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}
