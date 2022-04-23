using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    public class Jumper : MonoBehaviour
    {
        // from smallest
        [SerializeField] private List<SphereCollider> colliders;
        [SerializeField] private List<float> multipliers;
        
        public float GetJumpMultiplier(Ray ray, float distance)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if ((colliders[i].transform.position - ray.origin).magnitude < colliders[i].radius)
                {
                    return multipliers[i];
                }
            }

            return 0;
        }
    }
}