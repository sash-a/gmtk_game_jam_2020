using Code.Player;
using System;
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

    public Player player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponentInParent<Player>();
        MakeJellyVertices();

        CreateMesh();
        UpdateMesh();
    }

    void Update()
    {
        transform.position = player.transform.position;
        UpdateMesh();
    }

    public void UpdateMesh() {
        Vector3[] newPoses = new Vector3[vertexNum];
        for (int i = 0; i < vertexNum; i++)
        { // instantiate the vertex objects
            newPoses[i] = vertexObjects[i].transform.localPosition * player.grower.size/0.15f;
        }
        mesh.vertices = newPoses;
        //Recalculate the bounding volume of the Mesh from the vertices
        mesh.RecalculateBounds();
    }

    public void CreateMesh()
    {
        /*
         * creates the mesh outline
         */
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mf.mesh = mesh;

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
        //List<Vector3> normalsList = new List<Vector3> { };
        //for (int i = 0; i < triangles.Length; i++)
        //{
        //    normalsList.Add(-Vector3.forward);
        //}
        //Vector3[] normals = normalsList.ToArray();

        //initialise
        Vector3[] zeros = new Vector3[vertexNum];
        for (int i = 0; i < vertexNum; i++)
        {
            zeros[i] = Vector3.zero;
        }
        mesh.vertices = zeros;
        mesh.triangles = triangles;
        //mesh.normals = normals;
    }

    public void MakeJellyVertices()
    {
        /*
         * creates the vertex objects
         */    
        GetComponent<MeshRenderer>().material = material;

        for (int i = 0; i < vertexNum; i++)
        { // instantiate the vertex objects
            GameObject vertex = Instantiate(vertexPrefab);
            //vertex.GetComponent<SpringJoint2D>().distance = radius;
            vertex.transform.parent = transform;
            vertex.transform.localPosition = getVertexStartingLocalPos(i);

            vertexObjects.Add(vertex);
        }
       

        for (int i = 0; i < vertexNum; i++)
        {//connects hinges to adjacent vertex hinges
            int nextID = (i + 1) % vertexNum;
            vertexObjects[i].GetComponent<HingeJoint2D>().connectedBody = vertexObjects[nextID].GetComponent<Rigidbody2D>();
            vertexObjects[i].GetComponent<SpringJoint2D>().connectedBody = rb;
        }

    }

    private Vector3 getVertexStartingLocalPos(int i)
    {
        float angle = 2f * Mathf.PI * ((float)i / (float)vertexNum);
        float x = radius * Mathf.Sin(angle);
        float y = radius * Mathf.Cos(angle);
        return new Vector3(x, y, 0f);
    }
}
