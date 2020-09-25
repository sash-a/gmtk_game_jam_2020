using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : Block
{
    public static string SPAWN_BLOCK = "spawnBlock";

    public override void start()
    {
        base.start();
        if (GameManager.instance.designingLevel)
        {
            setSpawn();
        }
        else
        {
            spawnPlayer();
        }
    }

    private void setSpawn()
    {
        if (GameManager.instance.spawnBlockPlaced)
        {
            throw new System.Exception("cannot have more than one spawn block");
        }
        GameManager.instance.spawnBlockPlaced = true;
    }

    private void spawnPlayer()
    {
        setSpawn();
        Debug.Log("spawning player");
        GameObject player = Instantiate(GameManager.instance.playerHolderPrefab);
        Vector3 playerStartPos = transform.position;
        playerStartPos.z = -1;
        player.transform.position = playerStartPos;

        GameManager.instance.nPlayers = 1;
        GameManager.instance.spawnedPlayer = true;

        Destroy(gameObject, 0.5f);
    }

    public override string getTypeString()
    {
        return SPAWN_BLOCK;
    }
}
