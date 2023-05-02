using System;
using System.Collections;
using _Internal._Dev.Management.Scripts;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace _Internal._Dev.UI.Scripts
{
    public class LevelProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider pb;
        private int levelLength;
        private int progress;
        private void Awake()
        {
            EventManager.AddListener<GameProgressEvent>(OnProgressCall);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<GameProgressEvent>(OnProgressCall);
        }

        private void Start()
        {
            levelLength = VarSaver.LevelLength;
            progress = 0;
        }
        private void OnProgressCall(GameProgressEvent obj)
        {
            progress++;
        }

        private void Update()
        {
            float value = pb.value;
            value += ((float) progress / levelLength - value) * Time.deltaTime;
            pb.value = value;
        }
    }
}
