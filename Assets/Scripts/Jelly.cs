using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private Mesh mesh;
    public Vector3[] vertices;
    public int CenterPoint;
    public int verticiesCount;

    public List<GameObject> points;
    public GameObject toBeInstantiated;

    public float speed = 10;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        verticiesCount = vertices.Length;

        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject childObject = Instantiate(toBeInstantiated, transform.position + vertices[i], Quaternion.identity) as GameObject;
            childObject.transform.parent = gameObject.transform;
            points.Add(childObject);
        }

        GameObject centre = points[CenterPoint];
        for (int i = 0; i < points.Count; i++)
        {
            if (i != CenterPoint || points[i] != centre)
            {
                if (i == points.Count-1)
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
                }
                else
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                }
            }
            else
            {
                points[i].GetComponent<HingeJoint2D>().connectedBody = points[i].GetComponent<Rigidbody2D>();
            }
        }

    }

    void Update()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = points[i].transform.localPosition;
        }
        mesh.vertices = vertices;
    }

}
