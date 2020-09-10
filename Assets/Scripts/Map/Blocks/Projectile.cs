using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject spawner;//used to not collide with spawning object

    public GameObject spawnerParent { get { return spawner.transform.parent.gameObject; } }
    public GameObject spawnerGrandParent { get { return spawner.transform.parent.parent.gameObject; } }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("projectile collided with " + collision.gameObject + " spawner: " + spawner + " spawner parent: " + spawnerParent + " grandparent: " + spawnerGrandParent);
        if (collision.gameObject == spawner || collision.gameObject == spawnerParent || collision.gameObject == spawnerGrandParent)
        {// don't destroy when colliding with the spawner
            return;
        }
        PlayerController pc = collision.GetComponent<PlayerController>();
        if(pc!= null)
        {
            Destroy(pc.gameObject);
        }
        Destroy(gameObject);
    }

}
