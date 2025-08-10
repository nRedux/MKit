using UnityEditor;
using UnityEngine;

namespace MKit.Math.Entities
{
    public class LineClosestPoint : MonoBehaviour
    {
        public PointEntity a;
        public LineEntity b;

        public ClosestPointMode Display = (ClosestPointMode) ( 1 << (int) ClosestPointMode.ClosestPoint | 1 << (int) ClosestPointMode.Distance );


        private bool DisplayFlagSet( ClosestPointMode flags, ClosestPointMode flag )
        {
            return ( flags & flag ) != 0;
        }


        // Update is called once per frame
        void OnDrawGizmos()
        {
            if( a == null || b == null )
                return;
            if( Display == 0 )
                return;

            Vector3 testA = a.Point.Position;
            Line testB = b.Line;

            Vector3 closestOnLine = testB.ClosestPoint( testA );

            if( DisplayFlagSet( Display, ClosestPointMode.ClosestPoint ) )
            {
                GizmosEx.PushColor( Color.red );
                Gizmos.DrawWireSphere( closestOnLine, .1f );
                GizmosEx.PopColor();
            }

            if( DisplayFlagSet( Display, ClosestPointMode.Distance ) )
            {
                float distance = testB.DistanceSquared( a.Point.Position );
                Handles.Label( closestOnLine + ( closestOnLine - a.Point.Position ) * .5f, distance.ToString( "N4" ) );
            }

            GizmosEx.PushColor( Color.blue );
            Gizmos.DrawLine( closestOnLine, a.Point.Position );
            GizmosEx.PopColor();

        }

    }

}