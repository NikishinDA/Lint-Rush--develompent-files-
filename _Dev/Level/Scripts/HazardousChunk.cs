using _Internal._Dev.Level.Scripts.Interfaces;
using _Internal._Dev.Roller.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public class HazardousChunk : Chunk, IInteractable
    {
        public virtual void DoAction(RollerCollisionManager manager)
        {
            manager.HazardInteraction(true);
            manager.InteractionEffect(type);
            GetComponent<Collider>().enabled = false;
        }
    }
}
