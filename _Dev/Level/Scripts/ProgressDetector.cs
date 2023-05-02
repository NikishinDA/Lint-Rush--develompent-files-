using System;
using _Internal._Dev.Management.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public class ProgressDetector : MonoBehaviour
    {
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            EventManager.Broadcast(GameEventsHandler.GameProgressEvent);
        }
    }
}
