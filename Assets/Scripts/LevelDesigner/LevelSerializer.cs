using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSerializer : MonoBehaviour
{
    public string editorLevelName;
    public static string levelFolderPath = "/Levels/";

    public static string staticLevelName;

    public InputField levelNameInput;

    private void Start()
    {
        if (editorLevelName != "")
        {
            //has provided name from editor
            staticLevelName = editorLevelName;
        }
        if (staticLevelName == "" || staticLevelName == null) {
            return;
        }
        loadLevel(staticLevelName);
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
        checkSaveString(saveString);
        Debug.Log("saving level '" + levelName + "'");
        File.WriteAllText(getLevelFilePath(levelName), saveString);
    }

    private static void checkSaveString(string saveString)
    {
        if (!saveString.Contains(SpawnBlock.SPAWN_BLOCK)) {
            throw new Exception("no spawn block, cannot save");
        }
        if (!saveString.Contains(FinishBlock.FINISH_BLOCK))
        {
            throw new Exception("no finish block, cannot save");
        }
    }

    public void saveCurrentLevel()
    {
        staticLevelName = levelNameInput.text;
        if (staticLevelName == "") {
            throw new Exception("must provide a level name to serialiser");
        }
        save(staticLevelName);
    }

    public void EditLevel()
    {
        Debug.Log("editing level " + staticLevelName);
        Destroy(gameObject, 0.1f);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single); // level level editor
    }

    public void Restart()
    {
        Debug.Log("restarting level " + staticLevelName);
        Destroy(gameObject, 0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}