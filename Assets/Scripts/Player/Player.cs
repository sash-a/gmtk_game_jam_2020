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

        private SpriteRenderer _sprite;
        public SpriteRenderer sprite { get { if (_sprite == null) { _sprite = GetComponentInChildren<SpriteRenderer>(); } return _sprite; } }


        private void Awake()
        {
            pc = GetComponent<PlayerController>();
            grower = GetComponent<Grower>();
            splitter = GetComponent<Splitter>();
            shaker = GetComponent<ObjectShake>();
            death = GetComponent<Death>();

            AllSlimes.singleton.livingPlayers.Add(this);//registers player

            GameManager.instance.nPlayers += 1;
        }


        public int remainingMass { get { return (int)Mathf.Pow(2, splitter.maxSplits - splitter.nSplits); } }
    }
}
