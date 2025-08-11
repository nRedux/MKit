using System;
using JetBrains.Annotations;
using UnityEngine;

namespace MKit.Math
{

    [Serializable]
    public struct Ray : IMathConstruct
    {
        private const float RAY_LENGTH = 10;
        private Vector3 _origin;
        private Vector3 _direction;


        public Vector3 Origin => _origin;
        public Vector3 Direction => _direction * RAY_LENGTH;


        public Ray( Vector3 origin, Vector3 direction )
        {
            this._origin = origin;
            this._direction = direction;
        }


        public static implicit operator UnityEngine.Ray (Ray r )
        {
            return new UnityEngine.Ray( r._origin, r._direction );
        }
    
    }

}