using System;
using _Internal._Dev.Level.Scripts.Interfaces;
using _Internal._Dev.Roller.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public enum AttachType
    {
        Poop,
        FishBone,
        Apple,
        Bug
    }
    public class AttachableObjects : MonoBehaviour, IInteractable
    {
        public AttachType type;
        public Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void DoAction(RollerCollisionManager manager)
        {
            manager.AttachObject(this);
            GetComponent<Collider>().enabled = false;
        }
    }
}
