using Code.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBlobs : MonoBehaviour
{
    public static AllBlobs singleton; 
    public HashSet<PlayerController> livingPlayers;

    private void Awake()
    {
        livingPlayers = new HashSet<PlayerController>();
        singleton = this;
    }

    public Vector2 getHorizontalBounds() {
        float min = float.MaxValue;
        float max = float.MinValue;
        foreach (PlayerController pc in livingPlayers)
        {
            min = Mathf.Min(pc.transform.position.x, min);
            max = Mathf.Max(pc.transform.position.x, max);
        }
        return new Vector2(min, max);
    }
}
