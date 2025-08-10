using System;
using UnityEngine;

namespace MKit.Math
{
    [Serializable]
    public struct Line: IMathConstruct
    {
        [SerializeField]
        private Vector3 _origin;
        [SerializeField]
        private Vector3 _direction;


        public Vector3 Origin => _origin;
        public Vector3 Direction => _direction;


        public Line( Vector3 origin, Vector3 direction )
        {
            this._origin = origin;
            this._direction = direction;
        }
    }

}