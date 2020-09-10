using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Singleton instance of grid we will use throughout the project
public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public Grid<GridNode> grid;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int cellSize;
    [SerializeField] private Vector3 originPos;

    Dictionary<Vector3, GameObject> gridObjects = new Dictionary<Vector3, GameObject>();

    public static GridManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GridManager>();

                if (instance == null)
                {
                    GameObject container = new GameObject("GridManager");
                    instance = container.AddComponent<GridManager>();
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);


        grid = new Grid<GridNode>(width, height, cellSize, originPos,
            (Grid<GridNode> g, int x, int y) => new GridNode(g, x, y));
    }


    public Vector3 ValidateWorldGridPosition(Vector3 position)
    {
        grid.GetXY(position, out int x, out int y);
        return grid.GetWorldPosition(x, y);
    }

    public Dictionary<Vector3, GameObject> GetGridObjects()
    {
        return gridObjects;
    }

    public List<Vector3> GetGridObjectLocations()
    {
        List<Vector3> objLocations = new List<Vector3>(gridObjects.Keys);
        return objLocations;
    }

    public void AddGridObject(Vector3 pos, GameObject obj)
    {
        gridObjects.Add(pos, obj);
    }

    public void RemoveGridObject(Vector3 pos)
    {
        gridObjects.Remove(pos);
    }

    public GameObject GetGridObject(Vector3 position)
    {
        return gridObjects[position];
    }

    public bool IsOccupied(Vector3 location)
    {
        if (GetGridObjectLocations().Contains(location))
        {
            //PrintStruc();
            //Debug.Log("Grid Conflict location: " + location);
            //Debug.Log("Grid conflict structure:" + structures[location]);
            return true;
        }

        return false;
    }

    void PrintObj()
    {
        foreach (KeyValuePair<Vector3, GameObject> kvp in gridObjects)
        {
            Debug.Log("Key: " + kvp.Key + "Value: " + kvp.Value);
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public Vector3 GetOriginPos()
    {
        return originPos;
    }

    public int GetCellSize()
    {
        return cellSize;
    }


    public class GridNode
    {
        private Grid<GridNode> grid;
        private int x;
        private int y;

        public GridNode(Grid<GridNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;

            Vector3 worldPos00 = grid.GetWorldPosition(x, y);
            Vector3 worldPos10 = grid.GetWorldPosition(x + 1, y);
            Vector3 worldPos01 = grid.GetWorldPosition(x, y + 1);
            Vector3 worldPos11 = grid.GetWorldPosition(x + 1, y + 1);

            Debug.DrawLine(worldPos00, worldPos01, Color.white, 999f);
            Debug.DrawLine(worldPos00, worldPos10, Color.white, 999f);
            Debug.DrawLine(worldPos01, worldPos11, Color.white, 999f);
            Debug.DrawLine(worldPos10, worldPos11, Color.white, 999f);
        }
    }
}