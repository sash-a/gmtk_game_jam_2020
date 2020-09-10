using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

/// http://www.mikedoesweb.com/2012/camera-shake-in-unity/

public class ObjectShake : MonoBehaviour {
    
    public float shake_decay = 0.002f;
    public float shake_intensity = 50f;

    private float temp_shake_intensity = 0;
    private Rigidbody2D rb;
	

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update (){
        if (temp_shake_intensity > 0)
        {
            // transform.position += Random.insideUnitSphere * temp_shake_intensity;
            rb.AddForce(Random.insideUnitSphere * temp_shake_intensity);
            temp_shake_intensity -= shake_decay;
        }
    }
	
    public void Shake()
    {
        temp_shake_intensity = shake_intensity;

    }
}