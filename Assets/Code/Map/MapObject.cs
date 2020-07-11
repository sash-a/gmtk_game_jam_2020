using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public int x
    {
        get { return (int)transform.position.x; }
        set { transform.position = new Vector3(value, transform.position.y, transform.position.z); }
    }

    public int y
    {
        get { return (int)transform.position.y; }
        set { transform.position = new Vector3(transform.position.x, value, transform.position.z); }
    }

    public Vector2Int pos
    {
        get { return new Vector2Int(x,y); }
        set { x = value.x; y = value.y; }
    }

    public Vector2Int localPos {
        get { return new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.y); }
        set { transform.localPosition = new Vector3(value.x, value.y, transform.localPosition.z); } }

}
