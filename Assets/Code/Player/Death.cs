using System;
using UnityEngine;

namespace Code.Player
{
    public class Death: MonoBehaviour
    {
        public void Start()
        {
            Game.instance.nPlayers += 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Spikes"))
                Destroy(gameObject);
        }

        public void OnDestroy()
        {
            Game.instance.nPlayers -= 1;
        }
    }
}