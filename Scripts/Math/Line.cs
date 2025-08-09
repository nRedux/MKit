using System;
using UnityEngine;

namespace MKit.Math
{
    [Serializable]
    public struct Line
    {
        public Vector3 Origin;
        public Vector3 Direction;

        public Line( Vector3 origin, Vector3 direction )
        {
            this.Origin = origin;
            this.Direction = direction;
        }
    }

}