using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{

    private LevelDesignControls levelDesignControls;
    public GameObject square;

    private void Awake()
    {
        levelDesignControls = new LevelDesignControls();
        square = Instantiate(square, Vector3.zero, Quaternion.identity);
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
        Vector2 mousePos = levelDesignControls.LevelDesign.Position.ReadValue<Vector2>();
        square.transform.position = GridManager.instance.ValidateWorldGridPosition(Util.GetMouseWorldPosition(new Vector3(mousePos.x, mousePos.y, 0), Camera.main));
        square.transform.position += new Vector3(1, 1, 0) * GridManager.instance.GetCellSize() * .5f;

        //print(ValidateWorldGridPosition(Util.GetMouseWorldPosition(new Vector3(mousePos.x, mousePos.y, 0), Camera.main)));
        //print(square.transform.position);

    }
}
