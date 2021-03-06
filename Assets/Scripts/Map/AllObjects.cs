﻿using System;
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
        mapObject.transform.parent = Map.singleton.objects.transform;
    }

    public void refreshArgs() {
        foreach (MapObject obj in allObjects)
        {
            obj.parseArgs(obj.args);
        }
    }

    internal string getSaveString()
    {
        string save = "{";
        foreach (MapObject obj in allObjects)
        {
            if (obj == null || obj.gameObject == null) {
                //has been deleted
                continue;
            }
            bool isChunk = obj.GetType() == typeof(BlockGroup) || obj.GetType().IsSubclassOf(typeof(BlockGroup));
            if (isChunk) { // don't save chunks
                continue;
            }
            if (save[save.Length -1] != '{') {
                save += ",";
            }
            save += obj.getSaveString();
        }
        save += "}";
        return save;
    }
}
