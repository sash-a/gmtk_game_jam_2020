using System;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelDesigner : MonoBehaviour
{
    static string TEMP_LEVEL_NAME = "%%tempLevel%%";

    public GameObject selectionSquare;
    public GameObject spawnableObject;
    public ArgsManager argsManager;
    LevelSerializer serialiser;

    private Vector3 mouseGridPos;

    private void Awake()
    {
        selectionSquare = Instantiate(selectionSquare, Vector3.zero, Quaternion.identity);
        serialiser = GetComponent<LevelSerializer>();
    }

    private void Start()
    {
        GameManager.instance.controls.LevelDesign.Select.performed += _ => spawnBlock(mouseGridPos);
        GameManager.instance.controls.LevelDesign.Delete.performed += _ => removeBlock(mouseGridPos);
        GameManager.instance.controls.LevelDesign.Position.performed += updateMouseGridPos;
    }

    void Update()
    {
        updateSelectionSquare(mouseGridPos);
    }

    public void testLevel() {
        if (serialiser.levelNameInput.text == "") {
            LevelSerializer.staticLevelName = TEMP_LEVEL_NAME;
        }
        else{
            LevelSerializer.staticLevelName = serialiser.levelNameInput.text;
        }
        LevelSerializer.save(LevelSerializer.staticLevelName);//must save before switching scenes
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject, 0.1f);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single); // level loader
    }

    private void spawnBlock(Vector3 mouseGridPos)
    {
        //Debug.Log("spawning block at " + mouseGridPos);
        if (!GridManager.Instance.IsOccupied(mouseGridPos) &&
            GridManager.instance.inGrid((int) mouseGridPos.x, (int) mouseGridPos.y))
        {
            // is empty and in grid, can spawn
            GameObject spawnedObject = Instantiate(spawnableObject, mouseToWorldPos(mouseGridPos), Quaternion.identity);
            GridManager.Instance.AddGridObject(mouseGridPos, spawnedObject);

            MapObject spawnedMapObject = spawnedObject.GetComponent<MapObject>();
            spawnedMapObject.setAdjacecyString(GridManager.Instance.getAdjacencyString(mouseGridPos));

            Vector3 neighbourPos;

            for (int dim = 0; dim < 2; dim++) // update the surrounding blocks adjacency strings
            {
                for (int off = -1; off < 2; off++)
                {
                    neighbourPos = mouseGridPos;

                    if (off == 0) { continue; }
                    neighbourPos[dim] += off;
                    if (!GridManager.Instance.IsOccupied(neighbourPos))
                    {
                        continue;
                    }
                    MapObject neighbour = GridManager.Instance.GetGridObject(neighbourPos).GetComponent<MapObject>();
                    neighbour.setAdjacecyString(GridManager.Instance.getAdjacencyString(neighbourPos));
                }
            }
        }
    }

    private void removeBlock(Vector3 mouseGridPos)
    {
        if (GridManager.Instance.IsOccupied(mouseGridPos) &&
            GridManager.instance.inGrid((int) mouseGridPos.x, (int) mouseGridPos.y))
        {
            GridManager.instance.RemoveGridObject(mouseGridPos);
        }
    }

    private void updateSelectionSquare(Vector3 mouseGridPos)
    {
        if (GridManager.Instance.inGrid((int) mouseGridPos.x, (int) mouseGridPos.y))
        {
            selectionSquare.SetActive(true);
            selectionSquare.transform.position = mouseToWorldPos(mouseGridPos);
            argsManager.selectBlockAt(mouseGridPos);
        }
        else
        {
            selectionSquare.SetActive(false);
        }
    }

    private void updateMouseGridPos(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = ctx.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        mouseGridPos = GridManager.instance.ValidateWorldGridPosition(worldPos);
    }

    public void setBlockType(GameObject block)
    {
        //Debug.Log("setting block type to: " + block);
        spawnableObject = block;
    }

    public Vector3 mouseToWorldPos(Vector3 mousePos)
    {
        return mousePos + GridManager.cellOffset;
    }
}