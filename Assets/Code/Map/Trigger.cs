using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    TriggerBlock block;

    // Start is called before the first frame update
    void Start()
    {
        block = transform.parent.gameObject.GetComponent<TriggerBlock>();
        if (block == null) {
            throw new System.Exception("no triggerblock on parent:" + transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        block.trigger();
    }
}
