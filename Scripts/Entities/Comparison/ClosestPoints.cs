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
        Everything = 0x7FFFFFF
    }

    public class ClosestPoints : MonoBehaviour
    {
        public Entity a;
        public Entity b;

        public ClosestPointMode Display = ClosestPointMode.Everything;


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

            var testA = a.GetConstruct();
            var testB = b.GetConstruct();

            Vector3 closestToA, closestToB;
            if( !CalculateClosestPoints( testA, testB, out closestToA, out closestToB ) )
                return;

            if( DisplayFlagSet( Display, ClosestPointMode.ClosestPoint ) )
            {
                GizmosEx.PushColor( Color.red );
                Gizmos.DrawWireSphere( closestToA, .1f );
                Gizmos.DrawWireSphere( closestToB, .1f );
                GizmosEx.PopColor();
            }

            if( DisplayFlagSet( Display, ClosestPointMode.Distance ) )
            {
                float distance = Vector3.Distance( closestToA, closestToB );
                Handles.Label( closestToA + ( closestToB - closestToA ) * .5f, distance.ToString( "N4" ) );
            }

            GizmosEx.PushColor( Color.blue );
            Gizmos.DrawLine( closestToA, closestToB );
            GizmosEx.PopColor();
        }


        //Attempts to wrap and perform closest points test between two constructs
        private bool CalculateClosestPointsInternal( IMathConstruct a, IMathConstruct b, out Vector3 closestA, out Vector3 closestB  )
        {
            if( a is Line lineAndLineA && b is Line lineAndLineB )
            {
                lineAndLineA.ClosestPoints( lineAndLineB, out closestA, out closestB );
                return true;
            }
            else if( a is Line lineAndSegA && b is LineSegment lineAndSegB )
            {
                lineAndSegB.ClosestPoints( lineAndSegA, out closestA, out closestB );
                return true;
            }
            else if( a is Line lineAndPointA && b is Point lineAndPointB )
            {
                closestA = lineAndPointA.ClosestPoint( lineAndPointB.Position );
                closestB = lineAndPointB.Position;
                return true;
            }
            else if( a is LineSegment lineSegAndPointA && b is Point lineSegAndPointB )
            {
                closestA = lineSegAndPointA.ClosestPoint( lineSegAndPointB.Position );
                closestB = lineSegAndPointB.Position;
                return true;
            }

            closestA = closestB = Vector3.zero;
            return false;
        }


        public bool CalculateClosestPoints( IMathConstruct a, IMathConstruct b, out Vector3 closestA, out Vector3 closestB )
        {
            if( CalculateClosestPointsInternal( a, b, out closestA, out closestB ) )
                return true;
            else//Flip and try alternate pairing
                return CalculateClosestPointsInternal( b, a, out closestA, out closestB );
        }

    }
}