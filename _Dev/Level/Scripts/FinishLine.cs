using System;
using _Internal._Dev.Management.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] effects;
        private void OnTriggerEnter(Collider other)
        {
            var evt = GameEventsHandler.MinigameStartEvent;
            evt.Player = other.transform.root.gameObject;
            EventManager.Broadcast(evt);
            this.enabled = false;

            foreach (var effect in effects)
            {
                effect.Play();
            }
        }
    }
}
