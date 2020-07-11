using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        positionSpikes();
    }

    private void positionSpikes()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float camHeight = Map.singleton.mapCam.orthographicSize; //half
        float camWidth = Map.singleton.mapCam.aspect * camHeight; //half
        renderer.size = new Vector2(camWidth / transform.localScale.x * 2, renderer.size.y);
        transform.position = new Vector3(transform.position.x, -camHeight + 0.2f, transform.position.z);
        renderer.enabled = true;
    }
}
