using System;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelDesigner : MonoBehaviour
{
    public GameObject selectionSquare;
    public GameObject spawnableObject;

    private Vector3 mouseGridPos;

    private void Awake()
    {
        selectionSquare = Instantiate(selectionSquare, Vector3.zero, Quaternion.identity);
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

    private void spawnBlock(Vector3 mouseGridPos)
    {
        if (!GridManager.Instance.IsOccupied(mouseGridPos) &&
            GridManager.instance.inGrid((int) mouseGridPos.x, (int) mouseGridPos.y))
        {
            // is empty and in grid, can spawn
            GameObject spawnedObject = Instantiate(spawnableObject, mouseToWorldPos(mouseGridPos), Quaternion.identity);
            GridManager.Instance.AddGridObject(mouseGridPos, spawnedObject);
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
        if (GridManager.instance.inGrid((int) mouseGridPos.x, (int) mouseGridPos.y))
        {
            selectionSquare.SetActive(true);
            selectionSquare.transform.position = mouseToWorldPos(mouseGridPos);
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
        spawnableObject = block;
    }

    public Vector3 mouseToWorldPos(Vector3 mousePos)
    {
        return mousePos + new Vector3(1, 1, 0) * GridManager.instance.GetCellSize() * .5f;
    }
}