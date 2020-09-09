using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObjects : MonoBehaviour
{
    public HashSet<MapObject> allObjects;

    private void Start()
    {
    }

    public void registerObject(MapObject mapObject) {
        if (allObjects == null) {
            allObjects = new HashSet<MapObject>();
        }
        allObjects.Add(mapObject);
    }

    public void refreshArgs() {
        foreach (MapObject obj in allObjects)
        {
            obj.parseArgs(obj.args);
        }
    }
}
