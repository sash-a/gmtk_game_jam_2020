using Code.Player;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public Finished ufo;
    HashSet<PlayerController> caughtPlayers;
    SpriteRenderer renderer;

    static float tractionSpeed = 2;
    static float alphaFrequency = 2f;

    private void Start()
    {
        caughtPlayers = new HashSet<PlayerController>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public Vector3 ufoPos { get { return ufo.transform.GetChild(0).position; } }

    private void Update()
    {
        foreach (PlayerController blob in caughtPlayers)
        {
            if(blob == null)
            {
                continue;
            }
            //zeroth child of the UFO should be the ship body
            Vector3 move = (ufoPos - blob.transform.position).normalized * tractionSpeed;
            blob.transform.position = blob.transform.position + move*Time.deltaTime;
        }
        Color c = renderer.color;
        float sin = (Mathf.Sin(Time.time * alphaFrequency) + 1) * 0.5f; //between 0 and 1
        c.a = 0.2f + sin* 0.5f;
        renderer.color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc == null)
        {
            throw new System.Exception("no playercontroller on object " + collision.gameObject + " triggering trigger");
        }
        caughtPlayers.Add(pc);
        pc.rb.gravityScale = 0;//freeze it and add a constant force in dir of ufo
        pc.rb.velocity = Vector2.zero;

        pc.GetComponent<Grower>().growthRate = 0;
        pc.GetComponent<ObjectShake>().shake_intensity = 0;
    }
}
