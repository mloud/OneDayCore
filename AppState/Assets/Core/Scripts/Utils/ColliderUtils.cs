using UnityEngine;

namespace OneDay.Core.Utils
{
    public static class ColliderUtils
    {
        public static Vector3 GetRandomPoint(BoxCollider collider)
        {
            var xRnd = Random.Range(-collider.bounds.size.x * 0.5f, collider.bounds.size.x * 0.5f);
            var yRnd = Random.Range(-collider.bounds.size.y * 0.5f, collider.bounds.size.y * 0.5f);
            var zRnd = Random.Range(-collider.bounds.size.z * 0.5f, collider.bounds.size.z * 0.5f);

            return collider.transform.position + collider.center + new Vector3(xRnd, yRnd, zRnd);
        }
    }
}
