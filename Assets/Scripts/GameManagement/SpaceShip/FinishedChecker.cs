using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Player;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Finished : MonoBehaviour
{
    public static Finished instance;
    
    [HideInInspector] public int nFinished;

    public TextMeshProUGUI finishedCounter;
    public TextMeshProUGUI remainingCounter;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        finishedCounter.text = "Rescued: " + nFinished + " / " + GameManager.instance.requiredToFinish;
    }

    private void Update()
    {
        int remainingMass = AllSlimes.singleton.getTotalMassRemaining();
        remainingCounter.text = "Remaining: " + remainingMass ;

        int neededToWin = GameManager.instance.requiredToFinish - nFinished;
        if(neededToWin > remainingMass)
        {
            remainingCounter.color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            nFinished += player.remainingMass;
            finishedCounter.text = "Finished: " + nFinished + " / " + GameManager.instance.requiredToFinish;

            Destroy(other.gameObject);
        }
    }

}
