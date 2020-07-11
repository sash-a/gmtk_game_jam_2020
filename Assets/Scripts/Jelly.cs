﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private Mesh mesh;
    public Vector3[] verticies;
    public int CenterPoint;
    public int verticiesCount;

    public List<GameObject> points;
    public GameObject toBeInstantiated;

    public PolygonCollider2D polyCollider;
    public float radius= 5;
    public int vertexNum = 32;

    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        PolyMesh(radius, vertexNum);
        MakeMeshJelly();
    }

    void Update()
    {
        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = points[i].transform.localPosition;
        }
        mesh.vertices = verticies;
    }

    void PolyMesh(float radius, int n)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        //verticies
        List<Vector3> verticiesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < n; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / n);
            verticiesList.Add(new Vector3(x, y, 0f));
        }
        Vector3 [] verticies = verticiesList.ToArray();

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (n - 2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < verticies.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.normals = normals;

        //polyCollider
        polyCollider.pathCount = 1;

        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < n; i++)
        {
            pathList.Add(new Vector2(verticies[i].x, verticies[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);
    }

    void MakeMeshJelly()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verticies = mesh.vertices;
        verticiesCount = verticies.Length;

        for (int i = 0; i < verticies.Length; i++)
        {
            GameObject childObject = Instantiate(toBeInstantiated, transform.position + verticies[i], Quaternion.identity) as GameObject;
            childObject.transform.parent = gameObject.transform;
            points.Add(childObject);
        }

        //GameObject centre = points[CenterPoint];
        for (int i = 0; i < points.Count; i++)
        {
            //if (i != CenterPoint)
            //{
                if (i == points.Count - 1)
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
                }
                else
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                }
           // }
        }

        //points[CenterPoint].GetComponent<HingeJoint2D>().connectedBody = points[CenterPoint].GetComponent<Rigidbody2D>();
    }
}
