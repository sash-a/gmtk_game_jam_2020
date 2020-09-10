using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    public LevelDesignControls levelDesignControls;
    public GameObject selectionSquare;
    public GameObject spawnableObject;

    public string levelName;
    private void Awake()
    {
        levelDesignControls = new LevelDesignControls();
        selectionSquare = Instantiate(selectionSquare, Vector3.zero, Quaternion.identity);
    }

    private void OnEnable()
    {
        levelDesignControls.Enable();
    }

    private void OnDisable()
    {
        levelDesignControls.Disable();
    }


    void Update()
    {
        Vector3 mouseGridPos = getMouseGridPos();
        updateSelectionSquare(mouseGridPos);
        if (levelDesignControls.LevelDesign.Select.triggered)
        {
            spawnBlock(mouseGridPos);
        }
    }

    private void spawnBlock(Vector3 mouseGridPos)
    {
        if (!GridManager.Instance.IsOccupied(mouseGridPos)) {
            //is empty, can spawn
            GameObject spawnedObject = Instantiate<GameObject>(spawnableObject, mouseGridPos, Quaternion.identity);
            GridManager.Instance.AddGridObject(mouseGridPos, spawnedObject);
        }
    }

    private void updateSelectionSquare(Vector3 mouseGridPos)
    {
        selectionSquare.transform.position = mouseGridPos;
    }

    private Vector3 getMouseGridPos() {
        Vector2 mousePos = levelDesignControls.LevelDesign.Position.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        Vector3 mouseGridPos = GridManager.instance.ValidateWorldGridPosition(worldPos);
        return mouseGridPos + new Vector3(1, 1, 0) * GridManager.instance.GetCellSize() * .5f;
    }
}
