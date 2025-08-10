using UnityEngine;

namespace MKit.Math
{

    /// <summary>
    /// Provides utility for updating as often as a game object position changes by more than a given delta
    /// </summary>
    public class Vector3DeltaUpdate
    {
        private const float DEFAULT_MAX_DELTA = .05f;
        
        private Vector3 _lastPosition;

        private float _maxDelta = DEFAULT_MAX_DELTA;

        public bool Update(Vector3 vector )
        {
            bool result = Vector3.SqrMagnitude( _lastPosition - vector ) >= _maxDelta * _maxDelta;
            if( result )
                _lastPosition = vector;
            return result;
        }
    }


    [ExecuteAlways]
    public class LineEntity : Entity
    {
        
        public Line Line;

        private Vector3DeltaUpdate _position = new Vector3DeltaUpdate();
        private Vector3DeltaUpdate _rotation = new Vector3DeltaUpdate();

        private void OnDrawGizmos()
        {
            Line.DrawGizmo();
        }

        private void Update()
        {
            if( _position.Update( transform.position ) )
                RefreshLine();

            if( _rotation.Update( transform.rotation.eulerAngles ) )
                RefreshLine();
        }


        public override IMathConstruct GetConstruct()
        {
            return Line;
        }

        private void RefreshLine()
        {
            Line = new Line( transform.position, transform.forward );
        }
    }

}