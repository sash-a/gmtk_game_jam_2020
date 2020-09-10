using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderStuff : MonoBehaviour
{
    public Rigidbody2D rb;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.material.SetVector("_Velocity", rb.velocity / 50);
    }
}