using System.Collections.Generic;
using UnityEngine;

namespace MKit.Math
{
    public static class GizmosEx
    {
        private const float LINE_LENGTH = 1000;
        private const float RAY_LENGTH = 10000f;

        private static Stack<Color> _gizmosColorStack = new Stack<Color>();

        static GizmosEx()
        {
            _gizmosColorStack = new Stack<Color>();
            _gizmosColorStack.Push( Gizmos.color );
        }

        public static void PushColor( Color color )
        {
            _gizmosColorStack.Push( color );
            Gizmos.color = color;
        }

        public static void PopColor()
        {
            if( _gizmosColorStack.Count < 2 )
                return;
            _gizmosColorStack.Pop();
            Gizmos.color = _gizmosColorStack.Peek();
        }

        public static void DrawGizmo( this Line line )
        {
            Gizmos.DrawLine( line.Origin - line.Direction * LINE_LENGTH * .5f, line.Origin + line.Direction * LINE_LENGTH * .5f );
        }

        public static void DrawGizmo( this Ray ray )
        {
            Gizmos.DrawRay( ray.Origin, ray.Direction * RAY_LENGTH );
        }

        public static void DrawGizmo( this LineSegment segment )
        {
            Gizmos.DrawLine( segment.PointA, segment.PointB );
        }
    }

}