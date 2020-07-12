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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject + " triggered " + gameObject);
        Code.Player.PlayerController pc = collision.gameObject.GetComponent<Code.Player.PlayerController>();
        block.trigger(pc);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject + " untriggered " + gameObject);
        Code.Player.PlayerController pc = collision.gameObject.GetComponent<Code.Player.PlayerController>();
        block.untrigger(pc);
    }
}
