using System;
using MKit.Math;
using UnityEngine;

namespace MKit.Math
{


    public static partial class MathLineSegment
    {

        public static Vector3 Project( this LineSegment line, Vector3 vector )
        {
            float dirMagSq = line._direction.sqrMagnitude;
            float proj = vector.Dot( line._direction );
            return ( proj / dirMagSq ) * line._direction;
        }


        public static float Distance( this LineSegment segment, Vector3 point )
        {
            return MathF.Sqrt( segment.DistanceSquared( point ) );
        }


        public static float DistanceSquared( this LineSegment segment, Vector3 point )
        {
            Vector3 v = segment._pointB - segment.PointA;
            Vector3 w = point - segment.PointA;
            float proj = w.Dot( v );

            if( proj < 0f )
            {
                return w.sqrMagnitude;
            }
            else
            {
                float vSq = v.sqrMagnitude;
                if( proj >= vSq)
                {
                    return w.sqrMagnitude - 2f * proj + vSq;
                }
                else
                {
                    return w.sqrMagnitude - proj * proj / vSq;
                }
            }
        }


        public static float DistanceSquared( this LineSegment segment, Line line )
        {
            return line.DistanceSquared( segment );
        }


        public static float DistanceSquared( this LineSegment segment0, LineSegment segment1, out float s_c, out float t_c)
        {
            Vector3 v0 = segment0._pointB - segment0.PointA;
            Vector3 v1 = segment1._pointB - segment1.PointA;

            Vector3 w0 = segment0.PointA - segment1.PointA;
            float a = v0.sqrMagnitude;
            float b = v0.Dot( v1 );
            float c = v1.sqrMagnitude;
            float d = v0.Dot( w0 );
            float e = v1.Dot( w0 );

            float denom = a * c - b * b;
            // parameters to compute s_c, t_c
            float sn, sd, tn, td;

            // if denom is zero, try finding closest point on segment1 to origin0
            if( Mathf.Approximately( denom, 0f ) )
            {
                // clamp s_c to 0
                sd = td = c;
                sn = 0.0f;
                tn = e;
            }
            else
            {
                // clamp s_c within [0,1]
                sd = td = denom;
                sn = b * e - c * d;
                tn = a * e - b * d;

                // clamp s_c to 0
                if( sn < 0.0f )
                {
                    sn = 0.0f;
                    tn = e;
                    td = c;
                }
                // clamp s_c to 1
                else if( sn > sd )
                {
                    sn = sd;
                    tn = e + b;
                    td = c;
                }
            }

            // clamp t_c within [0,1]
            // clamp t_c to 0
            if( tn < 0.0f )
            {
                t_c = 0.0f;
                // clamp s_c to 0
                if( -d < 0.0f )
                {
                    s_c = 0.0f;
                }
                // clamp s_c to 1
                else if( -d > a )
                {
                    s_c = 1.0f;
                }
                else
                {
                    s_c = -d / a;
                }
            }
            // clamp t_c to 1
            else if( tn > td )
            {
                t_c = 1.0f;
                // clamp s_c to 0
                if( ( -d + b ) < 0.0f )
                {
                    s_c = 0.0f;
                }
                // clamp s_c to 1
                else if( ( -d + b ) > a )
                {
                    s_c = 1.0f;
                }
                else
                {
                    s_c = ( -d + b ) / a;
                }
            }
            else
            {
                t_c = tn / td;
                s_c = sn / sd;
            }

            // compute difference vector and distance squared
            Vector3 wc = w0 + s_c * v0 - t_c * v1;
            return wc.sqrMagnitude;
        }


        public static void ClosestPoints( this LineSegment segment0, LineSegment segment1, out Vector3 closest1, out Vector3 closest0 )
        {
            Vector3 v0 = segment0._pointB - segment0.PointA;
            Vector3 v1 = segment1._pointB - segment1.PointA;

            // compute intermediate parameters
            Vector3 w0 = segment0.PointA - segment1._pointB;
            float a = v0.sqrMagnitude;
            float b = v0.Dot( v1 );
            float c = v1.sqrMagnitude;
            float d = v0.Dot( w0 );
            float e = v1.Dot( w0 );

            float denom = a * c - b * b;
            // parameters to compute s_c, t_c
            float s_c, t_c;
            float sn, sd, tn, td;

            // if denom is zero, try finding closest point on segment1 to origin0
            if( Mathf.Approximately( denom, 0 ) )
            {
                // clamp s_c to 0
                sd = td = c;
                sn = 0.0f;
                tn = e;
            }
            else
            {
                // clamp s_c within [0,1]
                sd = td = denom;
                sn = b * e - c * d;
                tn = a * e - b * d;

                // clamp s_c to 0
                if( sn < 0.0f )
                {
                    sn = 0.0f;
                    tn = e;
                    td = c;
                }
                // clamp s_c to 1
                else if( sn > sd )
                {
                    sn = sd;
                    tn = e + b;
                    td = c;
                }
            }

            // clamp t_c within [0,1]
            // clamp t_c to 0
            if( tn < 0.0f )
            {
                t_c = 0.0f;
                // clamp s_c to 0
                if( -d < 0.0f )
                {
                    s_c = 0.0f;
                }
                // clamp s_c to 1
                else if( -d > a )
                {
                    s_c = 1.0f;
                }
                else
                {
                    s_c = -d / a;
                }
            }
            // clamp t_c to 1
            else if( tn > td )
            {
                t_c = 1.0f;
                // clamp s_c to 0
                if( ( -d + b ) < 0.0f )
                {
                    s_c = 0.0f;
                }
                // clamp s_c to 1
                else if( ( -d + b ) > a )
                {
                    s_c = 1.0f;
                }
                else
                {
                    s_c = ( -d + b ) / a;
                }
            }
            else
            {
                t_c = tn / td;
                s_c = sn / sd;
            }

            // compute closest points
            closest0 = segment0.PointA + s_c * v0;
            closest1 = segment1.PointA + t_c * v1;
        }


        public static void ClosestPoints( this LineSegment segment, Line line, out Vector3 closestSegment, out Vector3 closestLine )
        {
            Vector3 sV = segment._pointB - segment.PointA;
            // compute intermediate parameters
            Vector3 w0 = segment.PointA - line.Origin;
            float a = sV.sqrMagnitude;
            float b = sV.Dot( line.Direction );
            float c = line.Direction.sqrMagnitude;
            float d = segment._direction.Dot( w0 );
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
                closestSegment = segment.PointA + s_c * segment._direction;
                closestLine = line.Origin + t_c * line.Direction;
            }

        }


        public static Vector3 ClosestPoint( this LineSegment segment, Vector3 point )
        {
            Vector3 v = segment._pointB - segment.PointA;
            Vector3 w = point - segment.PointA;
            float proj = w.Dot( v );

            if( proj < 0f )
            {
                return segment.PointA;
            }
            else
            {
                float vSq = v.sqrMagnitude;
                if( proj >= vSq )
                {
                    return segment._pointB;
                }
                else
                {
                    return segment.PointA + ( proj / vSq ) * v;
                }
            }
        }
    }

}