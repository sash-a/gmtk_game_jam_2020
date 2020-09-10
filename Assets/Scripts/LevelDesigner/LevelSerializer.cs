using System;
using System.IO;
using UnityEngine;

public class LevelSerializer : MonoBehaviour
{
    public string levelName;
    public static string levelFolderPath = "/Levels/";

    private void Start()
    {
        if (levelName == "" || levelName == null) {
            return;
        }
        string levelPath = getLevelFilePath(levelName);
        string saveString = File.ReadAllText(levelPath);
        load(saveString);
    }

    public static string getLevelFilePath(string levelName)
    {
        return Application.dataPath + levelFolderPath + levelName + ".json";
    }

    public static void load(string saveString)
    {
        saveString = saveString.Remove(0, 1).Remove(saveString.Length - 2); //removes enclosing braces
        string[] blocks = saveString.Split(new string[] {"},"}, StringSplitOptions.None);


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

    public static void save(string levelName)
    {
        Map.singleton.objects.refreshArgs();
        string saveString = Map.singleton.objects.getSaveString();
        File.WriteAllText(getLevelFilePath(levelName), saveString);
    }

    public void saveCurrentLevel()
    {
        save(levelName);
    }
}