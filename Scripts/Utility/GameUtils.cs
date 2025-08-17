using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MKit
{
    public static class GameUtils
    {
        private static readonly Vector3 ZERO = new Vector3( 0, 0, 0 );

        public static GameObject SpawnProjectile( GameObject prefab, Vector3 position, Vector3 velocity )
        {
            var inst = prefab.Duplicate( position, Quaternion.LookRotation( velocity, Vector3.up ) );
            var rb = inst.GetComponent<Rigidbody>();
            if( rb != null )
                rb.AddForce( velocity, ForceMode.VelocityChange );
            return inst;
        }

        


        #region INSTATIATION AND DESTRUCTION

        public static T Duplicate<T>( this T unityObject ) where T : Object
        {
            if( unityObject == null )
                return null;

            var inst = Object.Instantiate<T>( unityObject );

            return inst;
        }

        public static GameObject Duplicate( this GameObject unityObject, Vector3 position )
        {
            if( unityObject == null )
                return null;

            var inst = Object.Instantiate( unityObject, position, Quaternion.identity );

            return inst;
        }

        public static GameObject Duplicate( this GameObject unityObject, Vector3 position, Quaternion rotation )
        {
            if( unityObject == null )
                return null;

            var inst = Object.Instantiate( unityObject, position, rotation );

            return inst;
        }


        public static void DestroyChildren( this GameObject gameObject )
        {
            if( gameObject == null )
                return;
            Transform tr = gameObject.transform;
            for( int i = tr.childCount - 1; i >= 0; i-- )
            {
                var child = tr.GetChild( i );
                Object.Destroy( child.gameObject );
            }
        }


        public static void DestroyChildren( this Transform transform )
        {
            if( transform == null )
                return;
            for( int i = 0; i < transform.childCount; i++ )
            {
                var child = transform.GetChild( i );
                UnityEngine.Object.Destroy( child.gameObject );
            }
        }

        #endregion



        #region Camera
        public static bool PointInFrustum( this Camera camera, Vector3 pt )
        {
            var frustumPlanes = GeometryUtility.CalculateFrustumPlanes( camera );
            for( int i = 0; i < frustumPlanes.Length; i++ )
            {
                bool inFrustum = frustumPlanes[i].GetSide( pt );
                if( !inFrustum )
                    return false;
            }
            return true;
        }

        #endregion




        #region COMPONENTS

        public static IEnumerable<TInterfaceType> GetInterfaceComponents<TInterfaceType>( this GameObject obj ) where TInterfaceType : class
        {
            return obj.GetComponents<Component>().Where( x => typeof( TInterfaceType ).IsAssignableFrom( x.GetType() ) ).Select( x => x as TInterfaceType );
        }


        public static IEnumerable<TInterfaceType> GetInterfaceComponents<TInterfaceType>( this Component obj ) where TInterfaceType : class
        {
            return obj.gameObject.GetInterfaceComponents<TInterfaceType>();
        }

        public static IEnumerable<TInterfaceType> GetInterfaceComponentsInParent<TInterfaceType>( this GameObject obj ) where TInterfaceType : class
        {
            return obj.GetComponentsInParent<Component>().Where( x => typeof( TInterfaceType ).IsAssignableFrom( x.GetType() ) ).Select( x => x as TInterfaceType );
        }


        public static IEnumerable<TInterfaceType> GetInterfaceComponentsInParent<TInterfaceType>( this Component obj ) where TInterfaceType : class
        {
            return obj.gameObject.GetInterfaceComponentsInParent<TInterfaceType>();
        }

        public static IEnumerable<TInterfaceType> GetInterfaceComponentsInChildren<TInterfaceType>( this GameObject obj ) where TInterfaceType : class
        {
            return obj.GetComponentsInChildren<Component>().Where( x => typeof( TInterfaceType ).IsAssignableFrom( x.GetType() ) ).Select( x => x as TInterfaceType );
        }


        public static IEnumerable<TInterfaceType> GetInterfaceComponentsInChildren<TInterfaceType>( this Component obj ) where TInterfaceType : class
        {
            return obj.gameObject.GetInterfaceComponentsInChildren<TInterfaceType>();
        }

        public static T GetOrAddComponent<T>( this GameObject gameObject ) where T : Component
        {
            return gameObject.GetComponent<T>().Opt() ?? gameObject.AddComponent<T>().Opt();
        }
        #endregion


        #region
        public static void SetSprite( this Image image, Sprite sprite )
        {
            image.sprite = sprite;
        }
        #endregion



        #region CONVERSIONS

        public static Vector2Int ToVector2Int( this Vector2 vec2 )
        {
            return new Vector2Int( Mathf.FloorToInt( vec2.x ), Mathf.FloorToInt( vec2.y ) );
        }

        public static Vector2Int ToVector2Int( this Vector3 vec3 )
        {
            return new Vector2Int( Mathf.FloorToInt( vec3.x ), Mathf.FloorToInt( vec3.z ) );
        }

        #endregion



        #region RENDERERS
        public static Material GetMaterial( this GameObject gameObject )
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            if( r == null )
                return null;
            return r.material;
        }


        public static Material GetSharedMaterial( this GameObject gameObject )
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            if( r == null )
                return null;
            return r.sharedMaterial;
        }


        public static Material[] GetMaterials( this GameObject gameObject )
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            if( r == null )
                return null;
            return r.materials;
        }


        public static Material[] GetSharedMaterials( this GameObject gameObject )
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            if( r == null )
                return null;
            return r.sharedMaterials;
        }
        #endregion




        #region MISC.

        /// <summary>
        /// This sets your screen position within canvas space.
        /// </summary>
        /// <param name="transform">The screen position</param>
        public static void UIPositionOverWorld( this Transform transform, Vector3 worldPosition )
        {
            var _rectTransform = transform.GetComponent<RectTransform>();
            var _canvasRectTransform = transform.GetComponentInParent<Canvas>().Opt()?.GetComponent<RectTransform>();
            //_rectTransform.Opt()?.PositionOverWorld( _canvasRectTransform, worldPosition );
        }

        #endregion
    }

}