using System;
using System.Collections;
using UnityEngine;
using _Internal._Dev.Management.Scripts;

namespace _Internal._Dev.Level.Scripts
{
    public class CompleteAnimationController : MonoBehaviour
    {
        private Animator animator;
        [SerializeField] private GameObject camera;
        [SerializeField] private GameObject[] plushes;
        private GameObject plush;
        [SerializeField] private Transform plushHolder;
        [SerializeField] private Vector3 desiredRot;
        [SerializeField] private int numStabs;
        private SkinnedMeshRenderer renderer;
        private float toyProgress;
        [SerializeField] private float shapingTime;
        [SerializeField] private GameObject woolObject;
        private void Awake()
        {
            plush = plushes[VarSaver.ToyNumber];
            EventManager.AddListener<ShowToyEvent>(OnShowToy);
            animator = GetComponent<Animator>();
            renderer = plush.GetComponent<SkinnedMeshRenderer>();
            toyProgress = PlayerPrefs.GetFloat("ToyProgress", 0);
        }
        private void OnDestroy()
        {
            EventManager.RemoveListener<ShowToyEvent>(OnShowToy);

        }

        private void OnShowToy(ShowToyEvent obj)
        {
            woolObject.gameObject.SetActive(false);
            plush.SetActive(true);
            plushHolder.localScale = new Vector3(toyProgress, toyProgress, toyProgress);
            animator.SetTrigger("Present");
            camera.SetActive(true);
        }


        public void PlushStabbed()
        {
            Taptic.Medium();
            StartCoroutine(PlushShaping(shapingTime));
        }

        public void StabVibration()
        {
            Taptic.Medium();
        }
        private IEnumerator PlushShaping(float time)
        {
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                renderer.SetBlendShapeWeight(0, 100 * t/time);
                yield return null;
            }
            renderer.SetBlendShapeWeight(0, 100);
        }
        
        public void FinisherEnd()
        {
            var evt = GameEventsHandler.GameOverEvent;
            EventManager.Broadcast(evt);
        }
    }
}
