using Code.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBlobs : MonoBehaviour
{
    public static AllBlobs singleton; 
    public Controls controls;

    public HashSet<Player> livingPlayers;

    private void Awake()
    {
        livingPlayers = new HashSet<Player>();
        singleton = this;

        controls = new Controls();
        controls.Enable();
    }

    private void Update()
    {
        //Debug.Log("total remaining: " + getTotalMassRemaining()); 
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
}
