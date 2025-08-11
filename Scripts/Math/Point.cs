using UnityEngine;

[System.Serializable]
public struct Point: IMathConstruct
{
    private Vector3 _position;


    public Vector3 Position => _position;


    public Point( Vector3 components )
    {
        this._position = components;
    }


    public static implicit operator Vector3(Point point )
    {
        return point.Position;
    }
}
