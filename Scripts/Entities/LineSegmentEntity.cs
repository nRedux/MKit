using System;
using System.Security.Cryptography;
using MKit.Math.Entities;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

namespace MKit.Math
{

    [ExecuteAlways]
    public class LineSegmentEntity : Entity
    {
        private const string GAMEOBJECT_NAME = "Line Segment";

        [HideInInspector]
        [SerializeField]
        public PointEntity PointA, PointB;

        [HideInInspector]
        [SerializeField]
        private LineSegment _segment;

        private void OnDrawGizmos()
        {
            _segment.DrawGizmo();
        }

        private void OnEnable()
        {
            _segment = new LineSegment( PointA.transform.position, PointB.transform.position );
            InitializePointEntities();
            gameObject.name = GAMEOBJECT_NAME;

            if( PointA != null )
                PointA.PositionUpdate = OnEndpointUpdate;
            if( PointB != null )
                PointB.PositionUpdate = OnEndpointUpdate;
        }

        private void Start()
        {

        }

        public override IMathConstruct GetConstruct()
        {
            return _segment;
        }

        private void InitializePointEntities()
        {
            if( PointA == null )
            {
                PointA = PointEntity.Create( transform.position + Vector3.back * .5f, "Point A" );
                PointA.PositionUpdate = OnEndpointUpdate;
                PointA.transform.SetParent( transform );
            }

            if( PointB == null )
            {
                PointB = PointEntity.Create( transform.position + Vector3.forward * .5f, "Point B" );
                PointB.PositionUpdate =  OnEndpointUpdate;
                PointB.transform.SetParent( transform );
            }
        }

        private void OnEndpointUpdate( Vector3 vector )
        {
            _segment = new LineSegment( PointA.Point.Position, PointB.Point.Position );
        }

        private void Update()
        {

        }

        private void RefreshLine()
        {

        }
    }

}