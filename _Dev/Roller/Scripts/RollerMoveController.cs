using System;
using _Internal._Dev.Management.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Internal._Dev.Roller.Scripts
{
    public class RollerMoveController : MonoBehaviour
    {
        private Rigidbody rb;
        private int direction = -1;
        private RollerAnimController animController;

        [SerializeField] private float verticalSpeed;
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private float upperConstraint;
        [SerializeField] private float lowerConstraint;
        [SerializeField] private ParticleSystem[] spinEffects;
        private float vSpeed;
        private float hSpeed;
        private bool stopped;

       // [SerializeField] private ParticleSystem trail;
        //public bool trailed = false;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animController = new RollerAnimController(GetComponent<Animator>());
            EventManager.AddListener<GameStartEvent>(OnGameStart);
            EventManager.AddListener<GameOverEvent>(OnGameOver);
            EventManager.AddListener<MinigameStartEvent>(OnMinigameStart);
            EventManager.AddListener<DebugEvent>(OnDebugCall);
            hSpeed = 0;
            stopped = true;
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<GameStartEvent>(OnGameStart);
            EventManager.RemoveListener<GameOverEvent>(OnGameOver);
            EventManager.RemoveListener<MinigameStartEvent>(OnMinigameStart);
            EventManager.RemoveListener<DebugEvent>(OnDebugCall);
        }

        private void OnMinigameStart(MinigameStartEvent obj)
        {
            hSpeed = 0;
            stopped = true;
            animController.AnimationEnabled(false);
            foreach (var effect in spinEffects)
            {
                effect.Stop();
            }
            //cleanEffect.gameObject.SetActive(false);
        }

        private void OnDebugCall(DebugEvent obj)
        {
            horizontalSpeed = obj.HSpeed;
            verticalSpeed = obj.VSpeed;
        }

        private void OnGameOver(GameOverEvent obj)
        {
            hSpeed = 0;
            stopped = true;
        }

        private void OnGameStart(GameStartEvent obj)
        {
            hSpeed = horizontalSpeed;
            stopped = false;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !stopped)
            {
                direction = 1;
                animController.AnimationPlay();
                foreach (var effect in spinEffects)
                {
                    effect.Play();
                }
            }
            else if (Input.GetMouseButtonUp(0) && !stopped)
            {
                direction = -1;
                animController.AnimationStop(); 
                foreach (var effect in spinEffects)
                {
                    effect.Stop();
                }
                //cleanEffect.Stop();
            }
        }

        void FixedUpdate()
        {
            if ((transform.position.y < upperConstraint) && (direction == -1) ||
                (transform.position.y > lowerConstraint) && (direction == 1))
            {
                vSpeed = verticalSpeed;
            }
            else
            {
                vSpeed = 0;
            }
            if (!stopped)
                rb.MovePosition(rb.position + 
                            (direction * vSpeed * Vector3.down
                            + Vector3.forward * hSpeed)  
                            * Time.deltaTime);

        }

    }
}