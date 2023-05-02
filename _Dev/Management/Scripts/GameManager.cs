using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Internal._Dev.Management.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private float _playTimer;
        private void Awake()
        {
            VarSaver.ToyNumber = PlayerPrefs.GetInt("ToyNumber", 0);
            EventManager.AddListener<GameStartEvent>(OnGameStart);
            EventManager.AddListener<MinigameStartEvent>(OnMinigameStart);
            Time.timeScale = 1;
            GameAnalytics.Initialize();
        }


        private void OnDestroy()
        {
            EventManager.RemoveListener<GameStartEvent>(OnGameStart);
            EventManager.RemoveListener<MinigameStartEvent>(OnMinigameStart);
        }

        private void OnGameStart(GameStartEvent obj)
        {
            int level = PlayerPrefs.GetInt("Level", 1);
            GameAnalytics.NewProgressionEvent (
                GAProgressionStatus.Start,
                "Level_" + level);
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            for (;;)
            {
                _playTimer += Time.deltaTime;
                yield return null;
            }
        }
    
        private void OnMinigameStart(MinigameStartEvent obj)
        {
            int level = PlayerPrefs.GetInt("Level", 1);
            var status = GAProgressionStatus.Complete;
            GameAnalytics.NewProgressionEvent(
                status,
                "Level_" + level,
                "PlayTime_" + Mathf.RoundToInt(_playTimer));
        }


#if UNITY_EDITOR
        void Update()
        {
           if (Input.GetKey(KeyCode.R))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                PlayerPrefs.SetInt("Level", 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                PlayerPrefs.SetInt("Level", 2);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                PlayerPrefs.SetInt("Level", 3);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                PlayerPrefs.SetInt("Level", 4);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                PlayerPrefs.SetInt("Level", 5);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKey(KeyCode.Alpha6))
            {
                PlayerPrefs.SetInt("Level", 6);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKey(KeyCode.Alpha7))
            {
                PlayerPrefs.SetInt("Level", 7);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        #endif
    }
}