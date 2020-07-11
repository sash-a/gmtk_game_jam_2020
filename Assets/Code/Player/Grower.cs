using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;

public class Grower : MonoBehaviour
{
    public float growthRate;
    public float minSplitSize;
    public float maxSize;

    private void FixedUpdate()
    {
        if (TooBig())
            GetComponent<Splitter>().Split();
        
        gameObject.transform.localScale *= growthRate;
    }

    public bool TooBig()
    {
        return gameObject.transform.localScale.magnitude > maxSize;
    }

    public bool TooSmall()
    {
        return gameObject.transform.localScale.magnitude < minSplitSize;
    }
}
