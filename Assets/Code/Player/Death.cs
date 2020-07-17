using System;
using System.Collections;
using UnityEngine;

namespace Code.Player
{
    public class Death : MonoBehaviour
    {
        public ParticleSystem dieEffect;

        public void Start()
        {
            Game.instance.nPlayers += 1;
        }

        public void OnDestroy()
        {
            Instantiate(dieEffect, transform.position, transform.rotation).Play();
            Game.instance.nPlayers -= 1;

            AllBlobs.singleton.livingPlayers.Remove(GetComponent<Player>());
        }
    }
}