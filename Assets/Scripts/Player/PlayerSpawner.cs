using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [HideInInspector] public Vector3 playerStartPos;

    private void Start()
    {
        GameObject player = Instantiate(GameManager.instance.playerHolderPrefab);
        playerStartPos.z = -1;
        player.transform.position = playerStartPos;
    }
}
