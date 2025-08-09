using UnityEngine;
using MKit.Math;
using UnityEditor;
using System;


namespace MKit.Tests
{

    enum LineFuncTests
    {
        Distance,
        Closest,
    }

    [Serializable]
    public class TestLine
    {
        public Vector3 Origin;
        public Vector3 Direction;


        public TestLine()
        {
            this.Origin = Vector3.zero;
            this.Direction = Vector3.zero;
        }


        public TestLine( Vector3 origin, Vector3 direction )
        {
            this.Origin = origin;
            this.Direction = direction;
        }


        public static implicit operator Line( TestLine line )
        {
            return new Line( line.Origin, line.Direction );
        }
    }


    public class TestLineFuncs : MonoBehaviour
    {
        [SerializeField]
        private LineFuncTests _test;

        public TestLine A = new TestLine( Vector3.zero, Vector3.right );
        public TestLine B = new TestLine( Vector3.up * 2f, Vector3.forward );

        // Update is called once per frame
        void Update()
        {

        }


        private void OnDrawGizmos()
        {
            GizmosDrawLine( A );
            GizmosDrawLine( B );
            GizmosClosestPoints();
        }

        private void GizmosClosestPoints()
        {
            Line testA = A;
            Line testB = B;
            float distance = testA.DistanceSquared( testB );
            Vector3 closestToA, closestToB;
            testA.ClosestPoints( testB, out closestToA, out closestToB );
            GizmosEx.PushColor( Color.red );
            Gizmos.DrawWireSphere( closestToA, .1f );
            Gizmos.DrawWireSphere( closestToB, .1f );
            GizmosEx.PopColor();

            Handles.Label( closestToA + ( closestToB - closestToA ) * .5f, distance.ToString( "N4" ) );

            GizmosEx.PushColor( Color.blue );
            Gizmos.DrawLine( closestToA, closestToB );
            GizmosEx.PopColor();

        }


        private void GizmosDrawLine( Line line )
        {
            float length = 1000;
            Gizmos.DrawLine( line.Origin - line.Direction * length * .5f, line.Origin + line.Direction * length * .5f );
        }


    }

}