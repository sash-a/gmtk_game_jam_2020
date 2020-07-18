using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private Mesh mesh;
    public Vector3[] verticies;

    public List<GameObject> points;
    public GameObject toBeInstantiated;

    public float radius;
    public int vertexNum;

    public Material material;

    private Rigidbody2D rb;

    private Vector3[] hinge_connectedAnchor;
    private Vector3[] hinge_anchor;

    private Vector3[] spring_connectedAnchor;
    private Vector3[] spring_anchor;

    void Start()
    {    
        rb = GetComponent<Rigidbody2D>();
        PolyMesh(radius, vertexNum);
        MakeMeshJelly();

        
        hinge_connectedAnchor = new Vector3[vertexNum];
        hinge_anchor = new Vector3[vertexNum];

        for (int i = 0; i < vertexNum; i++)
        {
            if (points[i].GetComponent<HingeJoint2D>() != null)
            {
                hinge_connectedAnchor[i] = points[i].GetComponent<HingeJoint2D>().connectedAnchor;
                hinge_anchor[i] = points[i].GetComponent<HingeJoint2D>().anchor;
            }
        }
        
        spring_connectedAnchor = new Vector3[vertexNum];
        spring_anchor = new Vector3[vertexNum];

        for (int i = 0; i < vertexNum; i++)
        {
            if (points[i].GetComponent<SpringJoint2D>() != null)
            {
                spring_connectedAnchor[i] = points[i].GetComponent<SpringJoint2D>().connectedAnchor;
                spring_anchor[i] = points[i].GetComponent<SpringJoint2D>().anchor;
            }
        }
        
       // StartCoroutine("En");

    }

    void Update()
    {
        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = points[i].transform.localPosition;
        }
        
        mesh.vertices = verticies;

        
        for (int i = 0; i < vertexNum; i++)
        {
            if (points[i].GetComponent<HingeJoint2D>() != null)
            {
                points[i].GetComponent<HingeJoint2D>().connectedAnchor = hinge_connectedAnchor[i];
                points[i].GetComponent<HingeJoint2D>().anchor = hinge_anchor[i];
            }

            if (points[i].GetComponent<SpringJoint2D>() != null)
            {
                points[i].GetComponent<SpringJoint2D>().connectedAnchor = spring_connectedAnchor[i];
                points[i].GetComponent<SpringJoint2D>().anchor = spring_anchor[i];
            }
        }
        

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

    public void MakeMeshJelly()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verticies = mesh.vertices;
    
        GetComponent<MeshRenderer>().material = material;

        if (points.Count == 0)
        {
            for (int i = 0; i < verticies.Length; i++)
            {
                GameObject childObject = Instantiate(toBeInstantiated, transform.position + verticies[i], Quaternion.identity) as GameObject;
                childObject.transform.parent = gameObject.transform;
                points.Add(childObject);
            }
        }
       

        for (int i = 0; i < points.Count; i++)
        {
            if (i == points.Count - 1)
            {
                points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
                points[i].GetComponent<HingeJoint2D>().anchor = verticies[i];
            }
                
            else
            { 
                points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                points[i].GetComponent<HingeJoint2D>().anchor = verticies[i];

            }

            points[i].GetComponent<SpringJoint2D>().connectedBody = rb;
            
        }

    } 

    public void Enlarge(Vector3 scale)
    {
        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] += scale;
            points[i].transform.localPosition += scale;
            points[i].transform.localScale += scale;

            //hinge_connectedAnchor[i] += scale;
            //hinge_anchor[i] += scale;

            //spring_connectedAnchor[i] += scale;
            //spring_anchor[i] += scale;
        }
    }

    public void Enlarge(float scale)
    {
       
        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] *= scale;
            points[i].transform.localPosition *= scale;
            points[i].transform.localScale *= scale;
        }
    }


    IEnumerator En()
    {
        for (int i = 0; i < 50; i++)
        {
            Enlarge(new Vector3(0.010f, 0.010f, 0.010f));
            yield return new WaitForSeconds(0.05f);
        }

        Debug.Log("Done Growing");

    }

}
