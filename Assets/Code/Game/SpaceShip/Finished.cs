using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Player;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Finished : MonoBehaviour
{
    public static Finished instance;
    
    [HideInInspector] public int nFinished;

    public TextMeshProUGUI finishedCounter;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        finishedCounter.text = "Finished: " + nFinished + " / " + Game.instance.requiredToFinish;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerSplitter = other.gameObject.GetComponent<Splitter>();
            
            
            nFinished += (int) math.pow(2, playerSplitter.maxSplits - playerSplitter.nSplits);
            finishedCounter.text = "Finished: " + nFinished + " / " + Game.instance.requiredToFinish;

            Destroy(other.gameObject);
        }
    }

}
