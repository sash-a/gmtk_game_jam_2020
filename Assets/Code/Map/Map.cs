using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map singleton;
    public GameObject blockPerfab;

    public Camera mapCam;
    public float maxPlayerHeight;
    public void Start()
    {
        singleton = this;
        mapCam = GetComponentInChildren<Camera>();
        maxPlayerHeight = camTop;
    }

    private void Update()
    {
        camTop = maxPlayerHeight;
    }

    public float camTop {
        get { return mapCam.transform.position.y + mapCam.orthographicSize; }
        set { mapCam.transform.position = new Vector3(mapCam.transform.position.x, value - mapCam.orthographicSize, mapCam.transform.position.z); }
    }

    public float camBottom
    {
        get { return mapCam.transform.position.y - mapCam.orthographicSize; }
    }

    internal void reportPlayerHeight(float playerHeight)
    {
        maxPlayerHeight = Mathf.Max(maxPlayerHeight, playerHeight);
    }
}
