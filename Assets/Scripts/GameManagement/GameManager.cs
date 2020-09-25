/*
 * Handles game state transitions ie from designing to playing
 */

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public Controls controls;

        public bool designingLevel;
        public bool showDesignLevelButton;

        [HideInInspector] public int nPlayers;
        [SerializeField] private TextMeshProUGUI loseText;
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] public float exitDelay;

        [SerializeField] private GameObject playerPrefab;
        public GameObject playerHolderPrefab;

        [SerializeField] private GameObject playLevelButton;
        [SerializeField] private GameObject designLevelButton;

        [HideInInspector] public FinishLine finishLine;
        
        private void Awake()
        {
            instance = this;
            controls = new Controls();
            if (designingLevel)
                controls.LevelDesign.Enable();
            else
                controls.Player.Enable();

            nPlayers = 0;

            if (!designingLevel)
            {
                loseText.enabled = false;
                winText.enabled = false;
            }
            designLevelButton.SetActive(showDesignLevelButton);

            finishLine = GetComponent<FinishLine>();
        }

        void Update()
        {
            if (nPlayers == 0 && !designingLevel)
            {
                if (finishLine.nFinished < finishLine.requiredToFinish)
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

        public void PlayLevel()
        {
            //designingLevel = false;
            //var player = Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
            //player.transform.parent = playerHolder.transform;
            //controls.LevelDesign.Disable();
            //controls.Player.Enable();
            //playLevelButton.SetActive(false); 
            //designLevelButton.SetActive(true);
        }

        public void DesignLevel()
        {
            //designingLevel = true;
            //AllSlimes.singleton.destroyAllPlayers();
            //controls.LevelDesign.Enable();
            //controls.Player.Disable();
            //playLevelButton.SetActive(true);
            //designLevelButton.SetActive(false);
        }
    }
}