using _Internal._Dev.Level.Scripts.Interfaces;
using _Internal._Dev.Roller.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public class FlyTape : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject flyPrefab;
        [SerializeField] private Transform flySpawn;
        private void Start()
        {
            Instantiate(flyPrefab, flySpawn);
        }

        public void DoAction(RollerCollisionManager manager)
        {
            manager.HazardInteraction();
        }
    }
}
