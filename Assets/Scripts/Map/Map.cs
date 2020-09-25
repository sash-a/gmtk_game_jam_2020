using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map singleton;
    public AllObjects objects;

    public Camera mapCam;
    public float baseCamSize = 5;
    public float mapSpeed;//the passive screen rise speed

    [HideInInspector] public float maxPlayerHeight;//an independant count of the map height. rises via mapSpeed, unaffected by player actions
    private float baseMapHeight;
    static int playerOverhead = 2;

    internal static GameObject getBlockPrefab(string name)
    {
        return singleton.blockTypePrefabDict[name];
    }

    public List<GameObject> blockTypePrefabs;
    public Dictionary<String, GameObject> blockTypePrefabDict;

    Spikes spikes;


    public void Start()
    {
        singleton = this;
        mapCam = GetComponentInChildren<Camera>();
        spikes = GetComponentInChildren<Spikes>();
        maxPlayerHeight = camTop;
        baseMapHeight = camTop;

        createBlockTypeDict();
    }

    private void Update()
    {
        if (GameManager.instance.designingLevel) return;  // None of this should apply if we are designing the level
        
        camTop = Mathf.Max(maxPlayerHeight, baseMapHeight);
        baseMapHeight += Time.deltaTime * mapSpeed;

        if (!GameManager.instance.playerSpanwed)
        {
            return;
        }

        Debug.Log("player has been spawned, adjusting map");

        Vector2 blobBounds = AllSlimes.singleton.getHorizontalBounds();
        float boundsSize = (blobBounds.y - blobBounds.x) * 1.1f / mapCam.aspect;
        mapCam.orthographicSize = Mathf.Max(boundsSize, baseCamSize); // TODO is this needed
        mapCam.transform.position = new Vector3((blobBounds.y + blobBounds.x) / 2f, mapCam.transform.position.y, mapCam.transform.position.z);

        spikes.positionSpikes();
    }

    private void createBlockTypeDict()
    {
        blockTypePrefabDict = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in blockTypePrefabs) {
            Block block = prefab.GetComponent<Block>();
            blockTypePrefabDict.Add(block.getTypeString(), prefab);
        }
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
