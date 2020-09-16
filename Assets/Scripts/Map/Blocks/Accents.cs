using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accents : MonoBehaviour
{
    public Sprite topMask;
    public Sprite bottomMask;
    public Sprite partialLeftMask;
    public Sprite partialRightMask;

    public GameObject leftAccent;
    public GameObject rightAccent;

    public GameObject accentPrefab;

    GameObject accentsObj;

    static float stretchFac = 0.42f;
    static float translateFac = 0.07f;

    public void updateAccents(string adjacencyString) {
        // creates a single gmaeobject with children for all needed accent masks
        if (accentsObj != null) {
            Destroy(accentsObj);
            accentsObj = null;
        }

        leftAccent.SetActive(false);
        leftAccent.transform.localScale = Vector3.one;
        leftAccent.transform.localPosition = Vector3.zero;

        rightAccent.SetActive(false);
        rightAccent.transform.localScale = Vector3.one;
        rightAccent.transform.localPosition = Vector3.zero;


        List<GameObject> accents = new List<GameObject>();
        if (adjacencyString.Contains("u")) {
            accents.Add(getAccentWith(topMask));
        }
        if (adjacencyString.Contains("d")) {
            accents.Add(getAccentWith(bottomMask));
        }
        //for left and right panels, must strech them to fill in the missing info from top and bottom pannels

        
        if (adjacencyString.Contains("l"))
        {
            handleSideAccents(adjacencyString, partialLeftMask, leftAccent, accents);
        }

        if (adjacencyString.Contains("r")) {
            handleSideAccents(adjacencyString, partialRightMask, rightAccent, accents);
        }


        GameObject parent = new GameObject();
        parent.transform.parent = transform;
        parent.transform.localPosition = Vector3.zero;
        foreach (GameObject accObj in accents)
        {
            accObj.transform.parent = parent.transform;
            accObj.transform.localPosition = Vector3.zero;
        }
        accentsObj = parent;
    }

    void handleSideAccents(string adjacency, Sprite mask, GameObject sideAccent, List<GameObject> accents) {
        bool topAndBottom = adjacency.Contains("u") && adjacency.Contains("d");
        bool neither = !adjacency.Contains("u") && !adjacency.Contains("d");

        if (topAndBottom)
        {
            accents.Add(getAccentWith(mask));
        }
        else
        {
            //use and stretch non masked left accent
            sideAccent.SetActive(true);
            if (neither)
            {
                //full stretch
                sideAccent.transform.localScale = new Vector3(1, 1 + stretchFac, 1);
            }
            else {
                //has one or the other
                if (adjacency.Contains("u"))
                {
                    sideAccent.transform.localScale = new Vector3(1, 1 + stretchFac * 0.5f, 1);
                    sideAccent.transform.localPosition += new Vector3(0, - translateFac, 0);
                }
                if (adjacency.Contains("d"))
                {
                    sideAccent.transform.localScale = new Vector3(1, 1 + stretchFac * 0.5f, 1);
                    sideAccent.transform.localPosition += new Vector3(0, translateFac, 0);
                }
            }
        }
    }

    private GameObject getAccentWith(Sprite mask)
    {
        GameObject obj = Instantiate(accentPrefab);
        Accent acc = obj.GetComponent<Accent>();
        acc.setMask(mask);
        return obj;
    }
}
