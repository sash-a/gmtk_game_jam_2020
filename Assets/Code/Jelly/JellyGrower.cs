using Code.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyGrower : MonoBehaviour
{
    private Jelly jelly;
    private float growTimer = 0f;
    private float growTimerMax = 1f;

    public float growthRate;
    public float minSize;
    public float maxSize;

    private Vector3 growthVec;

    public bool grow = true;
    public bool autosplit = true;

    private JellySplitter splitter;

    public float shakePercent; // Percent of max size, at which player starts shaking
    private ObjectShake shake;

    void Start()
    {
        jelly = GetComponent<Jelly>();
        
        shake = GetComponent<ObjectShake>();
        splitter = GetComponent<JellySplitter>();

        growthRate *= Random.Range(1f, 1.05f);
        growthVec = new Vector3(growthRate, growthRate, growthRate);

        //StartCoroutine("E");
    }

    private void FixedUpdate()
    {
        //if (autosplit && TooBig())
            //splitter.Split();

        //if (AlmostTooBig())
            //shake.Shake();

        if (grow)
            jelly.Enlarge(1.0005f);
            //transform.localScale *= 1.0005f;
    }

    public bool TooBig()
    {
        return gameObject.transform.localScale.x > maxSize;
    }

    public bool AlmostTooBig()
    {
        return gameObject.transform.localScale.x > maxSize * shakePercent;
    }

    IEnumerator E()
    {
        for (int i = 0; i < 5; i++)
        {
            //jelly.Enlarge(1.15f);
            //jelly.Grow(1.15f);
            yield return new WaitForSeconds(2f);
        }

    }
}
