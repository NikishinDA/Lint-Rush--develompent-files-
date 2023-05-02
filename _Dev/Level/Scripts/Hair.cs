using _Internal._Dev.Level.Scripts.Interfaces;
using _Internal._Dev.Roller.Scripts;
using _Internal._Dev.Management.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public class Hair : MonoBehaviour, IInteractable
    {
        [SerializeField] private Color[] colors;
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            spriteRenderer.color = colors[VarSaver.ToyNumber];
        }
        public void DoAction(RollerCollisionManager manager)
        {
            if (manager.CollectHair())
            {
                EventManager.Broadcast(GameEventsHandler.PickUpHair);

                gameObject.SetActive(false);
            }
        }
    }
}
