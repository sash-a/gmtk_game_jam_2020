using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (block == null) {
            //hasnt started yet
            return;
        }
        Game.Player.PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null) {
            block.trigger(pc);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Game.Player.PlayerController pc = collision.gameObject.GetComponent<Game.Player.PlayerController>();
        block.untrigger(pc);
    }

}
