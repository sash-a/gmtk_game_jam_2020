using System;
using Code.Player;
using TMPro;
using UnityEngine;

namespace Code
{
    public class Game : MonoBehaviour
    {
        public static Game instance;
        
        [HideInInspector] public int nPlayers = 0;
        
        public TextMeshProUGUI loseText;
        public TextMeshProUGUI winText;
        public int requiredToFinish;

        private void Awake()
        {
            instance = this;
            nPlayers = 0;
            loseText.enabled = false;
            winText.enabled = false;
        }

        void Update()
        {
            if (nPlayers == 0)
            {
                if (Finished.instance.nFinished < requiredToFinish)
                    Lose();
                else
                    Win();
            }
        }

        private void Lose()
        {
            loseText.enabled = true;
        }

        private void Win()
        {
            winText.enabled = true;
        }
    }
}
