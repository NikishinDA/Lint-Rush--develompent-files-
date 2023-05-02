using System;
using System.Collections;
using System.Collections.Generic;
using _Internal._Dev.Level.Scripts;
using _Internal._Dev.Management.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Internal._Dev.Roller.Scripts
{
    public class RollerCollisionManager : MonoBehaviour
    {
        [SerializeField] private GameObject hairCylinder;
        [SerializeField] private GameObject innerCylinder;
        [SerializeField] private float scaleUnit;
        [SerializeField] private float damageMultiplier;
        [SerializeField] private float addMultiplier = 1f;
        [SerializeField] private float maxScaleLimit;
        [SerializeField] private float minScaleLimit;
        private List<AttachableObjects> attachedObjects;
        private SkinnedMeshRenderer renderer;
        private bool cooldown;
        [SerializeField] private float cooldownTime;
        private IEnumerator cooldownCor;
        [SerializeField] private Material[] materials;
        private RollerMoveController moveController;
        [SerializeField] private Color[] trailColors;
        [SerializeField] private ParticleSystem[] waterDrops;

        [SerializeField] private TrailController trailController;
        [SerializeField] private ParticleSystem[] emojiGood;
        [SerializeField] private ParticleSystem[] emojiBad;
        public bool emojiCooldown;
        public float Progress 
        {
            get
            {
                float value = (hairCylinder.transform.localScale.x - minScaleLimit) /
                (maxScaleLimit - minScaleLimit);
                value = Mathf.Clamp01(value);
                return value;
            }
        }

        public List<AttachableObjects> AttachedObjects
        {
            get
            {
                return attachedObjects;
            }
        }
        private void Awake()
        {
            EventManager.AddListener<DebugEvent>(OnDebugCall);
            EventManager.AddListener<MinigameStartEvent>(OnMinigameStart);
            renderer = hairCylinder.GetComponent<SkinnedMeshRenderer>();
            attachedObjects = new List<AttachableObjects>();
            moveController = GetComponent<RollerMoveController>();
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<DebugEvent>(OnDebugCall);
            EventManager.RemoveListener<MinigameStartEvent>(OnMinigameStart);
        }

        private void OnMinigameStart(MinigameStartEvent obj)
        {
            trailController.DisableTrail();
            foreach (var drop in waterDrops)
            {
                drop.Stop();
            }
        }

        private void Start()
        {
            hairCylinder.GetComponent<SkinnedMeshRenderer>().material = materials[VarSaver.ToyNumber];
        }
        private void OnDebugCall(DebugEvent obj)
        {
            addMultiplier = obj.PlusProg;
            damageMultiplier = obj.MinusProg;
        }

        public bool CollectHair()
        {
            if (hairCylinder.transform.localScale.x < maxScaleLimit && !cooldown)
            {
                ScaleByMulti(addMultiplier);
                //return true;
            }

            if (!emojiCooldown && !cooldown)
            {
                emojiGood[Random.Range(0,emojiGood.Length)].Play();
                emojiCooldown = true;
            }

            return !cooldown;
        }

        public bool HazardInteraction(bool runCooldown = false)
        {
            if (runCooldown)
            {
                if (cooldownCor != null)
                {
                    StopCoroutine(cooldownCor);
                }

                cooldownCor = CooldownTimer();
                StartCoroutine(cooldownCor);
            }
            Taptic.Failure();
            emojiBad[Random.Range(0,emojiBad.Length)].Play();
            //emojiHappy.Clear();
            if (hairCylinder.transform.localScale.x > minScaleLimit)
            {
                ScaleByMulti(-damageMultiplier);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<AttachableObjects> InteractionEffect(ChunkType type,
            AdditionalObstacle addType = AdditionalObstacle.None)
        {
            EventManager.Broadcast(GameEventsHandler.PickUpObstacle);
            
           trailController.EnableTrail(trailColors[(int) type - 1]);
            if (type == ChunkType.Sink)
            {
                List<AttachableObjects> disposeAttach = new List<AttachableObjects>();
                for (int i = attachedObjects.Count - 1; i > (attachedObjects.Count - 1) / 2; i--)
                {
                    disposeAttach.Add(attachedObjects[i]);
                }

                StartCoroutine(SinkInteractionDelay(attachedObjects, 0.5f));

                foreach (var drop in waterDrops)
                {
                    drop.Play();
                }
                return disposeAttach;
            }

            return null;
        }

        private IEnumerator SinkInteractionDelay(List<AttachableObjects> attached, float time)
        {
            yield return new WaitForSeconds(time);
            int attachedCount = attached.Count;
            for (int i = attachedCount - 1; i > (attachedCount - 1) / 2; i--)
            {
                attached[i].transform.SetParent(null);
                attached[i].rb.useGravity = true;
                attached[i].rb.isKinematic = false;
                attached.RemoveAt(i);
            }
        }
        public bool ShrinkByPercent(float percent)
        {
            float delta = percent * (maxScaleLimit - minScaleLimit);
            Vector3 scale = hairCylinder.transform.localScale;
            scale -= new Vector3(delta, 0, delta);
            hairCylinder.transform.localScale = scale;
            float weight = renderer.GetBlendShapeWeight(1);
            renderer.SetBlendShapeWeight(1, weight - percent * 100);

            return hairCylinder.transform.localScale.x > minScaleLimit;
        }

        public void AttachObject(AttachableObjects obj)
        {
            attachedObjects.Add(obj);
            obj.transform.SetParent(innerCylinder.transform, true);
        }

        public void DetachAll()
        {
            foreach (var t in attachedObjects)
            {
                t.transform.SetParent(null);
                t.rb.useGravity = true;
                t.rb.isKinematic = false;
                t.rb.AddForce( Random.insideUnitSphere, ForceMode.Impulse);
            }

            attachedObjects.Clear();
        }
        private void ScaleByMulti(float multi)
        {
            Vector3 scale = hairCylinder.transform.localScale;
            scale += new Vector3(multi * scaleUnit, 0, multi * scaleUnit);
            hairCylinder.transform.localScale = scale;
            float weight = renderer.GetBlendShapeWeight(1);
            renderer.SetBlendShapeWeight(1, weight + multi * (1 / (maxScaleLimit - minScaleLimit)));
        }

        private IEnumerator CooldownTimer()
        {
            cooldown = true;
            for (float t = 0; t < cooldownTime; t += Time.deltaTime)
            {
                yield return null;
            }
            cooldown = false;
            trailController.DisableTrail();
            foreach (var drop in waterDrops)
            {
                drop.Stop();
            }
            
        }
    }
}