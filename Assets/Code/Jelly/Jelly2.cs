using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using UnityEngine;

public class Jelly2 : MonoBehaviour
{
    private Mesh mesh;
    public Vector3[] verticies;

    public List<GameObject> points;
    public GameObject toBeInstantiated;

    public float radius;
    private float radiusLastFrame;
    public int vertexNum;
    private int vertexNumLastFrame;

    public Material material;

    private Rigidbody2D rb;

    void Start()
    {    
        rb = GetComponent<Rigidbody2D>();
        rb.position = transform.position;
        GetComponent<MeshRenderer>().material = material;

        radiusLastFrame = radius;
        vertexNumLastFrame = vertexNum;

        PolyMesh(radius, vertexNum);
        MakeMeshJelly();

       
        StartCoroutine("Enlarge");
        //StartCoroutine("update");

    }

    private void FixedUpdate()
    {   
        
        if (radius != radiusLastFrame || vertexNum != vertexNumLastFrame)
        {
            PolyMesh(radius, vertexNum);
            MakeMeshJelly();
        }

        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = points[i].transform.localPosition;
        }

        mesh.vertices = verticies;

        radiusLastFrame = radius;
        vertexNumLastFrame = vertexNum;
        
    }


    public void PolyMesh(float radius, int n)
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
    }

    private void UpdateRadius(int n)
    {
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
        Vector3[] verticies = verticiesList.ToArray();
        mesh.vertices = verticies;
    }

    public void MakeMeshJelly()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verticies = mesh.vertices;
        
        if(points.Count == 0)
        {
            for (int i = 0; i < verticies.Length; i++)
            {
                GameObject childObject = Instantiate(toBeInstantiated, transform.position + verticies[i], Quaternion.identity) as GameObject;
                childObject.transform.parent = gameObject.transform;
                points.Add(childObject);
            }
        }
        else
        {

            for (int i = 0; i < vertexNum; i++)
            {
                //Debug.Log("index: " + i);

                if (points[i] != null)
                {
                    Destroy(points[i]);
                    points.RemoveAt(i);
                }

            }

            for (int i = 0; i < verticies.Length; i++)
            {
                GameObject childObject = Instantiate(toBeInstantiated, transform.position + verticies[i], Quaternion.identity) as GameObject;
                childObject.transform.parent = gameObject.transform;
                points.Add(childObject);
            }
           
            //Debug.Log("size? " + points.Count);
                      

            /*
            for (int i = 0; i < points.Count; i++)
            {
                points[i].transform.position = transform.position + verticies[i];
                points[i].transform.rotation = Quaternion.identity;
                points[i].transform.parent = gameObject.transform;
            }
            */
        }
        
              
        for (int i = 0; i < points.Count; i++)
        {
            if (i == points.Count - 1)
            {
                points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
            }
            else
            {
                points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
            }
                
            points[i].GetComponent<SpringJoint2D>().connectedBody = rb;
        }

    }

    void Grow(float growth)
    {
        radius += growth;
    }

    IEnumerator Enlarge()
    {
        for (int i = 0; i < 50; i++)
        {          
            Grow(.005f);
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("Done Growing");
    }

    IEnumerator update()
    {
        while(true)
        {
            if (radius != radiusLastFrame || vertexNum != vertexNumLastFrame)
            {
                PolyMesh(radius, vertexNum);
                MakeMeshJelly();
            }

            for (int i = 0; i < verticies.Length; i++)
            {
                verticies[i] = points[i].transform.localPosition;
            }

            mesh.vertices = verticies;

            radiusLastFrame = radius;
            vertexNumLastFrame = vertexNum;

            yield return new WaitForSeconds(0.1f);
        }
    }
}

    

