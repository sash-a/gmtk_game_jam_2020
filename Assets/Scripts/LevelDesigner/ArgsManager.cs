using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArgsManager : MonoBehaviour
{
    public InputField input;

    MapObject selectedBlock;
    string lastValue;

    private void Start()
    {
        lastValue = "";
    }

    private void Update()
    {
        if (selectedBlock == null) {
            return;
        }
        string nextValue = input.text;

        if (lastValue == nextValue) {
            return; //no change
        }

        selectedBlock.args = nextValue;
        lastValue = nextValue;
    }

    public void selectBlock(MapObject block) {
        if (block == null) {
            return;
        }
        selectedBlock = block;
        Debug.Log("selected block: " + block);
        setValue(block.args);
    }

    private void setValue(string args)
    {
        input.text = args;
        lastValue = args;
    }

    public void selectBlockAt(Vector3 pos) {
        if (!GridManager.Instance.IsOccupied(pos))
        {
            return;
        }
        GameObject blockObject = GridManager.Instance.GetGridObject(pos);
        MapObject block = blockObject.GetComponent<MapObject>();
        selectBlock(block);
    }
}
