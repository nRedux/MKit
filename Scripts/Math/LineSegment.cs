using System;
using UnityEngine;

namespace MKit.Math
{

    [Serializable]
    public struct LineSegment: IMathConstruct
    {
        public Vector3 _pointA;
        public Vector3 _pointB;
        public Vector3 _direction;


        public Vector3 PointA => _pointA;
        public Vector3 PointB => _pointA;
        public Vector3 Direction => _direction;


        public LineSegment( Vector3 pointA, Vector3 pointB )
        {
            this._pointA = pointA;
            this._pointB = pointB;
            this._direction = pointB - pointA;
        }
    }

}