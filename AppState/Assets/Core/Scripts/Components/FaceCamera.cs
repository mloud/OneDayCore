using UnityEngine;

namespace OneDay.Core.Components
{
    [ExecuteInEditMode]
    public class FaceCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
