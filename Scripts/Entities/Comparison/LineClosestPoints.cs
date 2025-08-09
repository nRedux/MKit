using System;
using MKit.Math;
using UnityEditor;
using UnityEngine;

namespace MKit.Math.Entities
{
    [Flags]
    public enum ClosestPointMode
    {
        None = 0,
        ClosestPoint = 1,
        Distance = 2,
    }

    public partial class LineClosestPoints : MonoBehaviour
    {
        public LineEntity a;
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

            Line testA = a.Line;
            Line testB = b.Line;

            Vector3 closestToA, closestToB;
            testA.ClosestPoints( testB, out closestToA, out closestToB );

            if( DisplayFlagSet( Display, ClosestPointMode.ClosestPoint ) )
            {
                GizmosEx.PushColor( Color.red );
                Gizmos.DrawWireSphere( closestToA, .1f );
                Gizmos.DrawWireSphere( closestToB, .1f );
                GizmosEx.PopColor();
            }

            if( DisplayFlagSet( Display, ClosestPointMode.Distance ) )
            {
                float distance = testA.DistanceSquared( testB );
                Handles.Label( closestToA + ( closestToB - closestToA ) * .5f, distance.ToString( "N4" ) );
            }

            GizmosEx.PushColor( Color.blue );
            Gizmos.DrawLine( closestToA, closestToB );
            GizmosEx.PopColor();

        }

    }
}