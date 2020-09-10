using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public string levelName;

    private void Start()
    {
        string levelPath = LevelDesigner.getLevelFilePath(levelName);
        string saveString = File.ReadAllText(levelPath);
        loadLevel(saveString);
    }

    public void loadLevel(string saveString)
    {
        saveString = saveString.Remove(0,1).Remove(saveString.Length - 2); //removes enclosing braces
        string[] blocks = saveString.Split(new string[] { "}," }, StringSplitOptions.None);


        foreach (string blockConf in blocks)
        {
            string type = blockConf.Split(':')[0];
            string args = blockConf.Split('{')[1].Split('}')[0]; // removes braces, type

            GameObject prefab = Map.singleton.blockTypePrefabDict[type];

            GameObject instance = Instantiate(prefab);
            instance.GetComponent<MapObject>().args = args;
            instance.transform.parent = Map.singleton.objects.transform;
        }
    }
}
