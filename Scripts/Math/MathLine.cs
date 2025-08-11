using System;
using MKit.Math;
using UnityEngine;

namespace MKit.Math
{


    public static partial class MathLine
    {

        #region Vector3 Extensions
        public static float Dot( this Vector3 a, Vector3 b )
        {
            return Vector3.Dot( a, b );
        }
        #endregion



        public static float Distance( this Line line0, Line line1 )
        {
            return MathF.Sqrt( line0.DistanceSquared( line1 ) );
        }


        public static float Distance( this Line line0, Ray ray )
        {
            return MathF.Sqrt( line0.DistanceSquared( ray ) );
        }


        public static float Distance( this Line line0, LineSegment lineSegment )
        {
            return MathF.Sqrt( line0.DistanceSquared( lineSegment ) );
        }

        public static float Distance( this Line line, Vector3 point )
        {
            return MathF.Sqrt( line.DistanceSquared( point ) );
        }


        public static float DistanceSquared( this Line line0, Line line1 )
        {
            Vector3 w0 = line0.Origin - line1.Origin;
            float a = line0.Direction.Dot( line0.Direction );
            float b = line0.Direction.Dot( line1.Direction );
            float c = line1.Direction.Dot( line1.Direction );
            float d = line0.Direction.Dot( w0 );
            float e = line1.Direction.Dot( w0 );

            float denom = a * c - b * b;
            if( Mathf.Approximately( denom, 0f ) )
            {
                Vector3 wc = w0 + ( ( b * e - c * d ) / denom ) * line0.Direction;
                return wc.sqrMagnitude;
            }
            else
            {
                Vector3 wc = w0 + ( ( b * e - c * d ) / denom ) * line0.Direction -
                    ( ( a * e - b * d ) / denom ) * line1.Direction;
                return wc.sqrMagnitude;
            }
        }



        public static float DistanceSquared( this Line line, Ray ray )
        {
            // compute intermediate parameters
            Vector3 w0 = ray.Origin - line.Origin;
            float a = ray.Direction.sqrMagnitude;
            float b = ray.Direction.Dot( line.Direction );
            float c = line.Direction.sqrMagnitude;
            float d = ray.Direction.Dot( w0 );
            float e = line.Direction.Dot( w0 );

            float denom = a * c - b * b;
            float s_c = 0;
            float t_c = 0;

            // if denom is zero, try finding closest point on segment1 to origin0
            if( Mathf.Approximately( denom, 0f ) )
            {
                s_c = 0.0f;
                t_c = e / c;
                // compute difference vector and distance squared
                Vector3 wc = w0 - t_c * line.Direction;
                return wc.Dot( wc );
            }
            else
            {
                // parameters to compute s_c, t_c
                float sn;

                // clamp s_c within [0,1]
                sn = b * e - c * d;

                // clamp s_c to 0
                if( sn < 0.0f )
                {
                    s_c = 0.0f;
                    t_c = e / c;
                }
                else
                {
                    s_c = sn / denom;
                    t_c = ( a * e - b * d ) / denom;
                }

                // compute difference vector and distance squared
                Vector3 wc = w0 + s_c * ray.Direction - t_c * line.Direction;
                return wc.sqrMagnitude;
            }

        }


        public static float DistanceSquared( this Line line, LineSegment segment )
        {
            // compute intermediate parameters
            Vector3 w0 = segment.PointA - line.Origin;
            float a = segment.Direction.sqrMagnitude;
            float b = segment.Direction.Dot( line.Direction );
            float c = line.Direction.sqrMagnitude;
            float d = segment.Direction.Dot( w0 );
            float e = line.Direction.Dot( w0 );

            float denom = a * c - b * b;
            float s_c = 0;
            float t_c = 0;
            // if denom is zero, try finding closest point on segment1 to origin0
            if( Mathf.Approximately( denom, 0f ) )
            {
                s_c = 0.0f;
                t_c = e / c;
                // compute difference vector and distance squared
                Vector3 wc = w0 - t_c * line.Direction;
                return wc.Dot( wc );
            }
            else
            {
                // parameters to compute s_c, t_c
                float sn;

                // clamp s_c within [0,1]
                sn = b * e - c * d;

                // clamp s_c to 0
                if( sn < 0.0f )
                {
                    s_c = 0.0f;
                    t_c = e / c;
                }
                // clamp s_c to 1
                else if( sn > denom )
                {
                    s_c = 1.0f;
                    t_c = ( e + b ) / c;
                }
                else
                {
                    s_c = sn / denom;
                    t_c = ( a * e - b * d ) / denom;
                }

                // compute difference vector and distance squared
                Vector3 wc = w0 + s_c * segment.Direction - t_c * line.Direction;
                return wc.sqrMagnitude;
            }
        }


        public static float DistanceSquared( this Line line, Vector3 point )
        {
            Vector3 w = point - line.Origin;
            float i = w.sqrMagnitude;
            float proj = w.Dot( line.Direction );
            float sqDir = line.Direction.sqrMagnitude;
            //If line length is normalized the division would be unnecessary
            return i - proj * proj / sqDir;
        }


        public static void ClosestPoints( this Line line0, Line line1, out Vector3 point0, out Vector3 point1 )
        {
            Vector3 w0 = line0.Origin - line1.Origin;
            float a = line0.Direction.Dot( line0.Direction );
            float b = line0.Direction.Dot( line1.Direction );
            float c = line1.Direction.Dot( line1.Direction );
            float d = line0.Direction.Dot( w0 );
            float e = line1.Direction.Dot( w0 );

            float denom = a * c - b * b;
            if( Mathf.Approximately( denom, 0f ) )
            {
                point0 = line0.Origin;
                point1 = line1.Origin + ( e / c ) * line1.Direction;
            }
            else
            {
                point0 = line0.Origin + ( ( b * e - c * d ) / denom ) * line0.Direction;
                point1 = line1.Origin + ( ( a * e - b * d ) / denom ) * line1.Direction;
            }
        }


        public static void ClosestPoints( this Line line, Ray ray, out Vector3 point0, out Vector3 point1 )
        {
            // compute intermediate parameters
            Vector3 w0 = ray.Origin - line.Origin;
            float a = ray.Direction.sqrMagnitude;
            float b = ray.Direction.Dot( line.Direction );
            float c = line.Direction.sqrMagnitude;
            float d = ray.Direction.Dot( w0 );
            float e = line.Direction.Dot( w0 );

            float denom = a * c - b * b;

            // if denom is zero, try finding closest point on ray1 to origin0
            if( Mathf.Approximately( denom, 0f ) )
            {
                // compute closest points
                point0 = ray.Origin;
                point1 = line.Origin + ( e / c ) * line.Direction;
            }
            else
            {
                // parameters to compute s_c, t_c
                float sn, s_c, t_c;

                // clamp s_c within [0,1]
                sn = b * e - c * d;

                // clamp s_c to 0
                if( sn < 0.0f )
                {
                    s_c = 0.0f;
                    t_c = e / c;
                }
                else
                {
                    s_c = sn / denom;
                    t_c = ( a * e - b * d ) / denom;
                }

                // compute closest points
                point0 = line.Origin + t_c * line.Direction;
                point1 = ray.Origin + s_c * ray.Direction;
            }
        }



        public static void ClosestPoints( this Line line, LineSegment segment, out Vector3 closestLine, out Vector3 closestSegment )
        {
            Vector3 sV = segment.PointB - segment.PointA;
            // compute intermediate parameters
            Vector3 w0 = segment.PointA - line.Origin;
            float a = sV.sqrMagnitude;
            float b = sV.Dot( line.Direction );
            float c = line.Direction.sqrMagnitude;
            float d = segment.Direction.Dot( w0 );
            float e = line.Direction.Dot( w0 );

            float denom = a * c - b * b;

            // if denom is zero, try finding closest point on line to segment origin
            if( Mathf.Approximately( denom, 0f ) )
            {
                // compute closest points
                closestSegment = segment.PointA;
                closestLine = line.Origin + ( e / c ) * line.Direction;
            }
            else
            {
                // parameters to compute s_c, t_c
                float s_c, t_c;
                float sn;

                // clamp s_c within [0,1]
                sn = b * e - c * d;

                // clamp s_c to 0
                if( sn < 0.0f )
                {
                    s_c = 0.0f;
                    t_c = e / c;
                }
                // clamp s_c to 1
                else if( sn > denom )
                {
                    s_c = 1.0f;
                    t_c = ( e + b ) / c;
                }
                else
                {
                    s_c = sn / denom;
                    t_c = ( a * e - b * d ) / denom;
                }

                // compute closest points
                closestSegment = segment.PointA + s_c * segment.Direction;
                closestLine = line.Origin + t_c * line.Direction;
            }
        }

        public static void ClosestPoints( this Line line, Vector3 point, out Vector3 point0, out Vector3 point1 )
        {
            Vector3 w = point - line.Origin;
            float vSq = line.Direction.sqrMagnitude;
            float proj = w.Dot( line.Direction );
            point0 = line.Origin + ( proj / vSq ) * line.Direction;
            point1 = point;
        }

    }

}