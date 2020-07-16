using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyVertex : MonoBehaviour
{
    private Rigidbody2D rb;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        count = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.forward * 100);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Split();
    }

    public void Split()
    {
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * 600);
    }
}
