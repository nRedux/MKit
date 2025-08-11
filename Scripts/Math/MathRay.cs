
using MKit.Math;
using UnityEngine;


namespace MKit.Math
{


    public static class MathRay
    {



        public static float DistanceSquared( this Ray ray, Line line )
        {
            // compute intermediate parameters
            Vector3 w0 = ray.Origin - line.Origin;
            float a = ray.Direction.sqrMagnitude;
            float b = ray.Direction.Dot( line.Direction );
            float c = line.Direction.sqrMagnitude;
            float d = ray.Direction.Dot( w0 );
            float e = line.Direction.Dot( w0 );

            //
            float denom = a * c - b * b;
            float s_c = 0f, t_c = 0f;
            // if denom is zero, try finding closest point on ray1 to origin0
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
                Vector3 wc = w0 + s_c * ray.Direction - t_c * line.Direction;
                return wc.Dot( wc );
            }
        }

        public static float DistanceSquared( this Ray ray0, Ray ray1 )
        {
            // compute intermediate parameters
            Vector3 w0 = ray0.Origin - ray1.Origin;
            float a = ray0.Direction.sqrMagnitude;
            float b = ray0.Direction.Dot( ray1.Direction );
            float c = ray1.Direction.sqrMagnitude;
            float d = ray0.Direction.Dot( w0 );
            float e = ray1.Direction.Dot( w0 );

            float denom = a * c - b * b;
            // parameters to compute s_c, t_c
            float sn, sd, tn, td;
            float s_c, t_c;
            // if denom is zero, try finding closest point on ray1 to origin0
            if( Mathf.Approximately( denom, 0f ) )
            {
                // clamp s_c to 0
                sd = td = c;
                sn = 0.0f;
                tn = e;
            }
            else
            {
                // clamp s_c within [0,+inf]
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
            }

            // clamp t_c within [0,+inf]
            // clamp t_c to 0
            if( tn < 0.0f )
            {
                t_c = 0.0f;
                // clamp s_c to 0
                if( -d < 0.0f )
                {
                    s_c = 0.0f;
                }
                else
                {
                    s_c = -d / a;
                }
            }
            else
            {
                t_c = tn / td;
                s_c = sn / sd;
            }

            // compute difference vector and distance squared
            Vector3 wc = w0 + s_c * ray0.Direction - t_c * ray1.Direction;
            return wc.Dot( wc );

        }



        public static float DistanceSquared( this Ray ray, LineSegment segment )
        {
            Vector3 w0 = segment.PointA - ray.Origin;
            float a = segment.Direction.sqrMagnitude;
            float b = segment.Direction.Dot( ray.Direction );
            float c = ray.Direction.sqrMagnitude;
            float d = segment.Direction.Dot( w0 );
            float e = ray.Direction.Dot( w0 );

            float denom = a * c - b * b;
            // parameters to compute s_c, t_c
            float sn, sd, tn, td;
            float s_c = 0f, t_c = 0f;
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

            // clamp t_c within [0,+inf]
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
            else
            {
                t_c = tn / td;
                s_c = sn / sd;
            }

            // compute difference vector and distance squared
            Vector3 wc = w0 + s_c * segment.Direction - t_c * ray.Direction;
            return wc.Dot( wc );
        }

        public static float DistanceSquared( this Ray ray, Vector3 point )
        {
            Vector3 w = point - ray.Origin;
            float proj = w.Dot( ray.Direction );

            if( proj < 0f )
            {
                return w.sqrMagnitude;
            }
            else
            {
                float vSq = ray.Direction.sqrMagnitude;
                return w.sqrMagnitude - proj * proj / vSq;
            }
        }

        public static void ClosestPoints( this Ray ray, Line line, out Vector3 closest0, out Vector3 closest1 )
        {
            line.ClosestPoints( ray, out closest0, out closest1 );
        }


        public static void ClosestPoints( this Ray ray0, Ray ray1, out Vector3 point0, out Vector3 point1 )
        {
            // compute intermediate parameters
            Vector3 w0 = ray0.Origin - ray1.Origin;
            float a = ray0.Direction.sqrMagnitude;
            float b = ray0.Direction.Dot( ray1.Direction );
            float c = ray1.Direction.sqrMagnitude;
            float d = ray0.Direction.Dot( w0 );
            float e = ray1.Direction.Dot( w0 );

            float denom = a * c - b * b;
            // parameters to compute s_c, t_c
            float s_c, t_c;
            float sn, sd, tn, td;

            // if denom is zero, try finding closest point on ray1 to origin0
            if( Mathf.Approximately( denom, 0f ) )
            {
                sd = td = c;
                sn = 0.0f;
                tn = e;
            }
            else
            {
                // start by clamping s_c
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
            }

            // clamp t_c within [0,+inf]
            // clamp t_c to 0
            if( tn < 0.0f )
            {
                t_c = 0.0f;
                // clamp s_c to 0
                if( -d < 0.0f )
                {
                    s_c = 0.0f;
                }
                else
                {
                    s_c = -d / a;
                }
            }
            else
            {
                t_c = tn / td;
                s_c = sn / sd;
            }

            // compute closest points
            point0 = ray0.Origin + s_c * ray0.Direction;
            point1 = ray1.Origin + t_c * ray1.Direction;
        }


        public static void ClosestPoints( this Ray ray, LineSegment segment, out Vector3 closest0, out Vector3 closest1 )
        {
            Vector3 w0 = segment.PointA - ray.Origin;
            float a = segment.Direction.sqrMagnitude;
            float b = segment.Direction.Dot( ray.Direction );
            float c = ray.Direction.Dot( ray.Direction );
            float d = segment.Direction.Dot( w0 );
            float e = ray.Direction.Dot( w0 );

            float denom = a * c - b * b;
            // parameters to compute s_c, t_c
            float s_c, t_c;
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

            // clamp t_c within [0,+inf]
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
            else
            {
                t_c = tn / td;
                s_c = sn / sd;
            }

            // compute closest points
            closest0 = ray.Origin + t_c * ray.Direction;
            closest1 = segment.PointA + s_c * segment.Direction;
        }



        public static void ClosestPoints( this Ray ray, Vector3 point, out Vector3 closest0, out Vector3 closest1 )
        {
            Vector3 w = point - ray.Origin;
            float proj = w.Dot( ray.Direction );
            closest1 = point;
            // endpoint 0 is closest point
            if( proj <= 0.0f )
                closest0 = ray.Origin;
            else
            {
                float vsq = ray.Direction.sqrMagnitude;
                // else somewhere else in ray
                closest0 = ray.Origin + ( proj / vsq ) * ray.Direction;
            }
        }
    }

}