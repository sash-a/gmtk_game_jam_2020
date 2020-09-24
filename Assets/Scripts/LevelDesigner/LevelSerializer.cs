using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSerializer : MonoBehaviour
{
    public string levelName;
    public static string levelFolderPath = "/Levels/";

    public InputField levelNameInput;
    private void Start()
    {
        if (levelName == "" || levelName == null) {
            return;
        }
        loadLevel(levelName);
    }

    public static string getLevelFilePath(string levelName)
    {
        return Application.dataPath + levelFolderPath + levelName + ".json";
    }

    public static void loadLevel(string levelName) {
        string levelPath = getLevelFilePath(levelName);
        string saveString = File.ReadAllText(levelPath);
        load(saveString);
        Debug.Log("loaded level " + levelName);
    }

    public static void load(string saveString)
    {
        /*
         * create the blocks and sets their args
         */
        saveString = saveString.Remove(0, 1).Remove(saveString.Length - 2); //removes enclosing braces
        string[] blocks = saveString.Split(new string[] {"},"}, StringSplitOptions.None);


        foreach (string blockConf in blocks)
        {
            string type = blockConf.Split(':')[0];
            string args = blockConf.Split('{')[1].Split('}')[0]; // removes braces, type

            if (!Map.singleton.blockTypePrefabDict.ContainsKey(type)) {
                throw new Exception("type " + type + " not found in map type dict.");
            }
            GameObject prefab = Map.singleton.blockTypePrefabDict[type];

            GameObject instance = Instantiate(prefab);
            instance.GetComponent<MapObject>().args = args;
            instance.transform.parent = Map.singleton.objects.transform;
        }
    }

    public static void save(string levelName)
    {
        string saveString = Map.singleton.objects.getSaveString();
        Debug.Log("saving level '" + levelName + "'");
        File.WriteAllText(getLevelFilePath(levelName), saveString);
    }

    public void saveCurrentLevel()
    {
        levelName = levelNameInput.text;
        if (levelName == "") {
            throw new Exception("must provide a level name to serialiser");
        }
        save(levelName);
    }
}