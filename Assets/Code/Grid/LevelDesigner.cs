﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{

    private LevelDesignControls levelDesignControls;
    public GameObject selectionSquare;
    public GameObject spawnableObject;

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
        //if (Input.GetMouseButton(0))
        //{
        //    spawnBlock(mouseGridPos);
        //}
    }

    public void saveLevel()
    {
        Map.singleton.objects.refreshArgs();
        string saveString = Map.singleton.objects.getSaveString();
        string filePath = Application.dataPath + "/Levels/levelTest.json";
        File.WriteAllText(filePath, saveString);
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
        selectionSquare.transform.position += new Vector3(1, 1, 0) * GridManager.instance.GetCellSize() * .5f;
    }

    private Vector3 getMouseGridPos() {
        Vector2 mousePos = levelDesignControls.LevelDesign.Position.ReadValue<Vector2>();
        Vector3 worldPos = Util.GetMouseWorldPosition(new Vector3(mousePos.x, mousePos.y, 0), Camera.main);
        Vector3 mouseGridPos = GridManager.instance.ValidateWorldGridPosition(worldPos);
        return mouseGridPos;
    }
}
