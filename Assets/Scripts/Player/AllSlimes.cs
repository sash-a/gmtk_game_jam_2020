using System;
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class AllSlimes : MonoBehaviour
{
    public static AllSlimes singleton; 

    public HashSet<Player> livingPlayers;

    private void Awake()
    {
        livingPlayers = new HashSet<Player>();
        singleton = this;
    }

    public Vector2 getHorizontalBounds() {
        float min = float.MaxValue;
        float max = float.MinValue;
        foreach (Player player in livingPlayers)
        {
            min = Mathf.Min(player.transform.position.x, min);
            max = Mathf.Max(player.transform.position.x, max);
        }
        return new Vector2(min, max);
    }

    public int getTotalMassRemaining() {
        int tot = 0;
        foreach (Player player in livingPlayers)
        {
            tot += player.remainingMass;
        }
        return tot;
    }

    public void destroyAllPlayers()
    {
        foreach (Player player in livingPlayers)
        {
            Destroy(player.gameObject);
        }
    }
}
