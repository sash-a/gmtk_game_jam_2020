using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = true;

        positionSpikes();
    }

    public void positionSpikes()
    { 
        float camWidth = Map.singleton.mapCam.aspect * Map.singleton.mapCam.orthographicSize * Map.singleton.mapCam.aspect; //half
        renderer.size = new Vector2(camWidth / transform.localScale.x * 2, renderer.size.y);

        float spikesHeight = Map.singleton.mapCam.transform.position.y - Map.singleton.baseCamSize -0.5f;
        transform.position = new Vector3(transform.position.x, spikesHeight , transform.position.z);
    }
}