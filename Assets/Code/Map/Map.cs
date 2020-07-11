using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map singleton;

    public Camera mapCam;
    public float maxPlayerHeight;
    static int playerOverhead = 2;

    internal static GameObject getBlockPrefab(string name)
    {
        return singleton.blockTypePrefabDict[name];
    }

    public List<GameObject> blockTypePrefabs;
    public Dictionary<String, GameObject> blockTypePrefabDict;


    public void Start()
    {
        singleton = this;
        mapCam = GetComponentInChildren<Camera>();
        maxPlayerHeight = camTop;

        createBlockTypeDict();
    }

    private void createBlockTypeDict()
    {
        blockTypePrefabDict = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in blockTypePrefabs) {
            Block block = prefab.GetComponent<Block>();
            
            blockTypePrefabDict.Add(block.getTypeString(), prefab);
        }
    }

    private void Update()
    {
        camTop = maxPlayerHeight;
    }

    public float camTop {
        get { return mapCam.transform.position.y + mapCam.orthographicSize; }
        set { mapCam.transform.position = new Vector3(mapCam.transform.position.x, value - mapCam.orthographicSize, mapCam.transform.position.z); }
    }

    public float camBottom
    {
        get { return mapCam.transform.position.y - mapCam.orthographicSize; }
    }

    internal void reportPlayerHeight(float playerHeight)
    {
        maxPlayerHeight = Mathf.Max(maxPlayerHeight, playerHeight + playerOverhead);
    }
}
