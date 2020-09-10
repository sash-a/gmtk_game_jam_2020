using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [HideInInspector] public PlayerController pc;
        [HideInInspector] public Grower grower;
        [HideInInspector] public Splitter splitter;
        [HideInInspector] public ObjectShake shaker;
        [HideInInspector] public Death death;
        [HideInInspector] public SpriteRenderer sprite;


        private void Awake()
        {
            pc = GetComponent<PlayerController>();
            grower = GetComponent<Grower>();
            splitter = GetComponent<Splitter>();
            shaker = GetComponent<ObjectShake>();
            death = GetComponent<Death>();
            sprite = GetComponent<SpriteRenderer>();

            AllSlimes.singleton.livingPlayers.Add(this);//registers player
        }


        public int remainingMass { get { return (int)Mathf.Pow(2, splitter.maxSplits - splitter.nSplits); } }
    }
}
