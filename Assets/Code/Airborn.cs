using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class Airborn : MonoBehaviour
{
    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
            controller.airborn = false;
    }
}
