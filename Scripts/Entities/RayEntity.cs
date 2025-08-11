using UnityEngine;

namespace MKit.Math
{

    [ExecuteAlways]
    public class RayEntity : Entity
    {
        
        public Ray Ray;

        private Vector3DeltaUpdate _position = new Vector3DeltaUpdate();
        private Vector3DeltaUpdate _rotation = new Vector3DeltaUpdate();

        private void OnEnable()
        {
            Ray = new Ray( transform.position, transform.forward );
        }

        private void OnDrawGizmos()
        {
            Ray.DrawGizmo();
        }

        private void Update()
        {
            if( _position.Update( transform.position ) )
                RefreshRay();

            if( _rotation.Update( transform.rotation.eulerAngles ) )
                RefreshRay();
        }


        public override IMathConstruct GetConstruct()
        {
            return Ray;
        }

        private void RefreshRay()
        {
            Ray = new Ray( transform.position, transform.forward );
        }
    }

}