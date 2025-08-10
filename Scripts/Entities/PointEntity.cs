using UnityEngine;
using UnityEngine.Events;

namespace MKit.Math.Entities
{

    [System.Serializable]
    public class Vector3Event: UnityEvent<Vector3>{}

    [System.Serializable]
    public class PointEvent : UnityEvent<Point> { }


    [ExecuteAlways]
    public class PointEntity : Entity
    {
        public Point Point => new Point( transform.position );

        private Vector3DeltaUpdate _positionDeltaUpdate = new Vector3DeltaUpdate();

        public System.Action<Vector3> PositionUpdate = null;

        private void OnEnable()
        {
        }

        public static PointEntity Create( Vector3 position, string name = null  )
        {
            GameObject gameObject = new GameObject( name ?? "pointEntity" );
            var pointEntity = gameObject.AddComponent<PointEntity>();
            //pointEntity.PositionUpdate = new Vector3Event();
            pointEntity.transform.position = position;
            return pointEntity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere( transform.position, .1f );
        }

        private void Update()
        {
            CheckPositionUpdate();
        }

        private void CheckPositionUpdate()
        {
            if( _positionDeltaUpdate.Update( transform.position ) )
            {
                PositionUpdate?.Invoke( transform.position );
            }
        }

        public override IMathConstruct GetConstruct()
        {
            return Point;
        }
    }

}