using _Internal._Dev.Management.Scripts;
using UnityEngine;

namespace _Internal._Dev.UI.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject overlay;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private GameObject toyScreen;
        [SerializeField] private GameObject tutorial;
        [SerializeField] private GameObject minigameScreen;
        private int level;
        private void Awake()
        {
            EventManager.AddListener<GameStartEvent>(OnGameStart);
            EventManager.AddListener<GameOverEvent>(OnGameOver);
            EventManager.AddListener<ShowToyEvent>(OnShowToy);
            EventManager.AddListener<MinigameStartEvent>(OnMinigameStart);
            level = PlayerPrefs.GetInt("Level", 1);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<GameStartEvent>(OnGameStart);
            EventManager.RemoveListener<GameOverEvent>(OnGameOver);
            EventManager.RemoveListener<ShowToyEvent>(OnShowToy);
            EventManager.RemoveListener<MinigameStartEvent>(OnMinigameStart);
        }

        private void OnMinigameStart(MinigameStartEvent obj)
        {
            minigameScreen.SetActive(true);
        }

        private void OnGameOver(GameOverEvent obj)
        {
            overlay.SetActive(false);            
            minigameScreen.SetActive(false);
            endScreen.SetActive(true);
        }

        private void Start()
        {
            startScreen.SetActive(true);
        }

        private void OnGameStart(GameStartEvent obj)
        {
            startScreen.SetActive(false);
            overlay.SetActive(true);
            if (level == 1)
            {
               tutorial.SetActive(true); 
            }
        }

        private void OnShowToy(ShowToyEvent obj)
        {
            endScreen.SetActive(false);
            toyScreen.SetActive(true);
        }
    }
}
