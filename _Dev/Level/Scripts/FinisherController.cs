using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Internal._Dev.Management.Scripts;
using _Internal._Dev.Roller.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Internal._Dev.Level.Scripts
{
    public class FinisherController : MonoBehaviour
    {
        private Animator animator;
        private static readonly int Soak = Animator.StringToHash("Soak");
        private bool ready = false;
        private RollerCollisionManager manager;
        [SerializeField] private GameObject woolObject;
        [SerializeField] private float shrinkPercent = 0.1f;
        [SerializeField] private GameObject[] toys;
        private float toyProgress;
        private int toyNumber;
        [SerializeField] private float progressPerLevel = 0.4f;
        [SerializeField] private ParticleSystem splashEffect;

        [SerializeField] private float spawnRadius;
        [SerializeField] private float invalRadius;
        private bool firstSoak = true;
        [SerializeField] private Transform spawnRect;
        [SerializeField] private Animator spawnRectAnimator;
        [SerializeField] private Animator woolPullerAnimator;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [SerializeField] private Material[] materials;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            
            EventManager.AddListener<MinigameStartEvent>(OnMinigameStart);

            toyProgress = PlayerPrefs.GetFloat("ToyProgress", 0);
            VarSaver.ToyOldProgress = toyProgress;
            toyNumber = PlayerPrefs.GetInt("ToyNumber", 0);
      
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<MinigameStartEvent>(OnMinigameStart);

        }
        private void Start()
        {
            //woolObject.transform.localScale = new Vector3(toyProgress, toyProgress, toyProgress);
            //toys[toyNumber].SetActive(true);
            meshRenderer.material = materials[toyNumber];
        }
        private void OnMinigameStart(MinigameStartEvent obj)
        {
            manager = obj.Player.GetComponent<RollerCollisionManager>();
            toyProgress += manager.Progress * progressPerLevel;
        }

        void Update()
        {
            if (ready)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetTrigger(Soak);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    animator.ResetTrigger(Soak);
                }
            }
        }

        public void RollerInPosition()
        {
            ready = true;
        }

        private void SpawnAttachables(List<AttachableObjects> attaches)
        {
            foreach (var attach in attaches)
            {
                attach.rb.useGravity = false;
                attach.rb.isKinematic = true;
                Transform attachTransform = attach.transform;
                attachTransform.SetParent(spawnRect);
                float rad = Random.Range(invalRadius, spawnRadius);
                int angle = Random.Range(0, 360);
                attachTransform.localPosition = new Vector3(rad * Mathf.Cos(angle),0,rad * Mathf.Sin(angle));
            }
        }

        private IEnumerator AttachablesEffect(float time)
        {
            List<AttachableObjects> attached = manager.AttachedObjects.ToList();
            yield return new WaitForSeconds(time);
            SpawnAttachables(attached);
            spawnRectAnimator.SetTrigger("Rise");
        }
        public void SoakRoller()
        {
            if (firstSoak)
            {
                StartCoroutine(AttachablesEffect(0.1f));
                manager.DetachAll();
                firstSoak = false;
            }

            if (manager.ShrinkByPercent(shrinkPercent))
            {
                if (woolObject.transform.localScale.x < 1)
                {
                    woolObject.transform.localScale += Vector3.one * shrinkPercent * progressPerLevel;
                }

                splashEffect.Play();
            }
            else
            {
                ready = false;
                animator.ResetTrigger(Soak);
                animator.SetTrigger("Hide");
                PlayerPrefs.SetFloat("ToyProgress", toyProgress);
                PlayerPrefs.Save();
                woolPullerAnimator.SetTrigger("End");
            }
        }

    }
}
