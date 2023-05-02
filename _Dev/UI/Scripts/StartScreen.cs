using System;
using _Internal._Dev.Management.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Internal._Dev.UI.Scripts
{
    public class StartScreen : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        [SerializeField] private InputField hSpeedInput;
        [SerializeField] private InputField vSpeedInput;
        [SerializeField] private InputField plusProg;
        [SerializeField] private InputField minusProg;
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartButtonPress);

           // hSpeedInput.text = PlayerPrefs.GetFloat("DebugHSpeed", 2f).ToString();
           // vSpeedInput.text = PlayerPrefs.GetFloat("DebugVSpeed", 10f).ToString();
            //plusProg.text = PlayerPrefs.GetFloat("PlusProg", 1).ToString();
            //minusProg.text = PlayerPrefs.GetFloat("MinusProg", 5).ToString();
        }

        private void OnStartButtonPress()
        {

            /*var evt = GameEventsHandler.DebugEvent;
            Single.TryParse(hSpeedInput.text, out evt.HSpeed);
            Single.TryParse(vSpeedInput.text, out evt.VSpeed);
            Single.TryParse(plusProg.text, out evt.PlusProg);
            Single.TryParse(minusProg.text, out evt.MinusProg);
            PlayerPrefs.SetFloat("DebugHSpeed", evt.HSpeed);
            PlayerPrefs.SetFloat("DebugVSpeed", evt.VSpeed);
            PlayerPrefs.SetFloat("PlusProg", evt.PlusProg);
            PlayerPrefs.SetFloat("MinusProg", evt.MinusProg);*/
            //PlayerPrefs.Save();
            //EventManager.Broadcast(evt);
            
            EventManager.Broadcast(GameEventsHandler.GameStartEvent);
        }
    }
}
