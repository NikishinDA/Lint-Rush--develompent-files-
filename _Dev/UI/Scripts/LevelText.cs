using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Internal._Dev.UI.Scripts
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] private Text levelText;

        private void Start()
        {
            int level = PlayerPrefs.GetInt("Level", 1);
            levelText.text = "Level " + level;
        }
    }
}
