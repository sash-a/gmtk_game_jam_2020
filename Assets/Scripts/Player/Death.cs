using System;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class Death : MonoBehaviour
    {
        public ParticleSystem dieEffect;

        public void Start()
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.nPlayers += 1;
            }
        }

        public void OnDestroy()
        {
            Instantiate(dieEffect, transform.position, transform.rotation).Play();
            if (GameManager.instance != null)
            {

                GameManager.instance.nPlayers -= 1;

                AllSlimes.singleton.livingPlayers.Remove(GetComponent<Player>());
            }
        }
    }
}