using UnityEngine;

namespace MKit.Math
{
    public static class MathfEx
    {
        public static int Wrap( int value, int minValue, int maxValue )
        {
            if( minValue == maxValue )
                return minValue;
            if( value >= minValue && value <= maxValue )
                return value;

            int range = maxValue - minValue;
            int outBounds = value < minValue ? value - minValue : value - maxValue;

            int mod = outBounds % ( range + 1 );
            if( mod == 0 )
                return outBounds > 0 ? maxValue : minValue;
            return mod >= 0 ? minValue + mod - 1 : maxValue + mod + 1;
        }

        public static Vector3 Spiral( float t, float passes, float size, Vector3 forward, Vector3 right )
        {
            return ( right * Mathf.Cos( t * 2 * Mathf.PI * passes ) * size + forward * Mathf.Sin( t * 2 * Mathf.PI * passes ) * size ) * ( 1f - t );
        }

        public static float Wobble( float t, float passes, float MaxSize )
        {
            return Mathf.Sin( t * passes * Mathf.PI * 2 ) * MaxSize * ( 1 - t );
        }

        public static Vector3 WobbleDrunk( float t, float passesA, float passesB, float maxSizeA, float maxSizeB, Vector3 forward, Vector3 right )
        {
            return ( forward * Wobble( t, passesA, maxSizeA ) + right * Wobble( t, passesB, maxSizeB ) ) * ( 1 - t );
        }

        public static float EaseInOutSine( float t )
        {
            return -( Mathf.Cos( Mathf.PI * t ) - 1 ) / 2f;
        }


    }

}