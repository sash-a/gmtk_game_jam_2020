using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class Spikes : EffectBlock
{
    SpriteRenderer renderer;
    static string SPIKES = "spike";
    public bool camSpikes;

    void Start()
    {
        // Why!? just know where the render is!
        renderer = GetComponent<SpriteRenderer>();
        if (renderer == null) {
            renderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (renderer == null)
        {
            renderer = GetComponentInParent<SpriteRenderer>();
        }
        renderer.enabled = true;

        if (camSpikes)
        {
            positionSpikes();
        }
    }

    public void positionSpikes()
    { 
        float camWidth = Map.singleton.mapCam.aspect * Map.singleton.mapCam.orthographicSize * Map.singleton.mapCam.aspect; //half
        renderer.size = new Vector2(camWidth / transform.localScale.x * 2, renderer.size.y);

        float spikesHeight = Map.singleton.mapCam.transform.position.y - Map.singleton.baseCamSize -0.5f;
        transform.position = new Vector3(transform.position.x, spikesHeight , transform.position.z);
    }

    public override void affect(PlayerController pc)
    {
        Destroy(pc.gameObject);
    }

    public override void unaffect(PlayerController pc)
    {
    }

    public override string getTypeString()
    {
        return SPIKES;
    }
}