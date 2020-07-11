using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map singleton;
    public GameObject blockPerfab;
    public void Start()
    {
        singleton = this;
    }
}
