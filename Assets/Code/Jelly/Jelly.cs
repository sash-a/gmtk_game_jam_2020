using Code.Player;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private Mesh mesh;
    [HideInInspector] public List<GameObject> vertexObjects;

    public GameObject vertexPrefab;

    public float radius;
    public int vertexNum;

    public Material material;

    private Rigidbody2D rb;

    private Vector3[] hinge_connectedAnchor;
    private Vector3[] hinge_anchor;

    private Vector3[] spring_connectedAnchor;
    private Vector3[] spring_anchor;

    public Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PolyMesh();
        MakeJellyVertices();
    }

    void Update()
    {
        transform.position = player.transform.position;

        Vector3[] newVertexPositions = new Vector3[vertexNum];
        for (int i = 0; i < vertexNum; i++)
        {
            newVertexPositions[i] = vertexObjects[i].transform.localPosition;
        }
        mesh.vertices = newVertexPositions;
    }

    public void PolyMesh()
    {
        /*
         * creates the mesh outline
         */
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mf.mesh = mesh;

        //verticies
        List<Vector3> verticiesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < vertexNum; i++)
        {
            float angle = 2f * Mathf.PI * ((float)i / (float) vertexNum);
            x = radius * Mathf.Sin(angle);
            y = radius * Mathf.Cos(angle);
            verticiesList.Add(new Vector3(x, y, 0f));
        }
        Vector3 [] verticies = verticiesList.ToArray();

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (vertexNum - 2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < triangles.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        //mesh.normals = normals;
    }

    public void MakeJellyVertices()
    {
        /*
         * creates the vertex objects
         */
        mesh = GetComponent<MeshFilter>().mesh;
    
        GetComponent<MeshRenderer>().material = material;

        if (vertexNum != mesh.vertices.Length) {
            throw new System.Exception();
        }

        //if (vertexObjects.Count == 0)
        //{
        for (int i = 0; i < vertexNum; i++)
        { // instantiate the vertex objects
            GameObject childObject = Instantiate<GameObject>(vertexPrefab);
            childObject.transform.parent = transform;
            childObject.transform.localPosition = mesh.vertices[i];

            vertexObjects.Add(childObject);
        }
        //}
       

        for (int i = 0; i < vertexNum; i++)
        {//connects hinges to adjacent vertex hinges
            int nextID = (i + 1) % vertexNum;
            vertexObjects[i].GetComponent<HingeJoint2D>().connectedBody = vertexObjects[nextID].GetComponent<Rigidbody2D>();
            vertexObjects[i].GetComponent<SpringJoint2D>().connectedBody = rb;
        }

    } 

}
