using System;
using UnityEngine;
using MKit.Tests;

namespace MKit.Math
{

    [Serializable]
    public struct LineSegment
    {
        public readonly Vector3 PointA;
        public readonly Vector3 PointB;
        public readonly Vector3 Direction;
        public LineSegment( Vector3 pointA, Vector3 pointB )
        {
            this.PointA = pointA;
            this.PointB = pointB;
            this.Direction = pointB - pointA;
        }
    }

}