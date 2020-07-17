using Code.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject spawner;//used to not collide with spawning object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == spawner)
        {
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
