using System;
using System.Collections;
using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        [HideInInspector] public int nPlayers;
        
        public TextMeshProUGUI loseText;
        public TextMeshProUGUI winText;
        public int requiredToFinish;

        public float exitDelay;

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
            StartCoroutine(DelayMainMenuSwitch());
        }

        private void Win()
        {
            winText.enabled = true;
            StartCoroutine(DelaySceneSwitch());
        }

        private IEnumerator DelayMainMenuSwitch()
        {
            yield return new WaitForSeconds(exitDelay);
            SceneManager.LoadScene(0);
        }
        
        private IEnumerator DelaySceneSwitch()
        {
            yield return new WaitForSeconds(exitDelay);
            var desiredScene = SceneManager.GetActiveScene().buildIndex + 1;
            print(desiredScene);
            if (desiredScene > SceneManager.sceneCountInBuildSettings - 1)
                desiredScene = 0;
            SceneManager.LoadScene(desiredScene);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
