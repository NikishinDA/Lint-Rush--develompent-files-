using _Internal._Dev.Level.Scripts.Interfaces;
using UnityEngine;

namespace _Internal._Dev.Roller.Scripts
{
    public class RollerCollisionDetector : MonoBehaviour
    {
        [SerializeField] private RollerCollisionManager manager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
                interactable.DoAction(manager);
        }
    }
}
