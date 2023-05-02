using _Internal._Dev.Level.Scripts.Interfaces;
using _Internal._Dev.Roller.Scripts;
using _Internal._Dev.Management.Scripts;
using UnityEngine;

namespace _Internal._Dev.Level.Scripts
{
    public class Comb : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject hairObject;
        [SerializeField] private Material[] materials;
        private void Start()
        {
            MeshRenderer[] hairRenderers = hairObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in hairRenderers)
            {
                renderer.material = materials[VarSaver.ToyNumber];
            }
            
        }
        public void DoAction(RollerCollisionManager manager)
        {
            if (manager.HazardInteraction())
            {
                hairObject.SetActive(true);
            }
        }
    }
}
