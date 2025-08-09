using UnityEngine;

namespace MKit.Math.Entities
{

    [ExecuteAlways]
    public class PointEntity : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere( transform.position, .1f );
        }

        private void Update()
        {
        }

        private void RefreshLine()
        {
        }
    }

}