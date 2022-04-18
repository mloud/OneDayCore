using System;
using DG.Tweening;
using OneDay.Core;
using UnityEngine;

namespace OneDay.Games.Jumper
{
    public class SimpleCharacter : ABaseMono
    {
        public Action OnFinished;
        public Action OnFailed;
        
        [SerializeField] private CharacterController character;
        [SerializeField] private float MaxSpeed = 4;
        [SerializeField] private float Speed;
        [SerializeField] private float JumpForce;
        [SerializeField] private Vector3 playerMovementSpeed;
        [SerializeField] private bool AllowInAirJump;
        [SerializeField] private PlayerInput playerInput;
        
        [Range(0, 3)] [SerializeField] private float fallingMultiplier = 1;

        private Animator animator;
        private float ySpeed;
        private bool running;
        private bool finished;
        protected override void Awake()
        {
            base.Awake();
            character = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            running = true;
        }

        public void Kill()
        {
            running = false;
            OnFailed();
            PlayKilledSequence();
        }
        
        private void Update()
        {
            if (running)
            {
                playerMovementSpeed = new Vector3(0, 0, playerInput.GetMoveValue());
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            Vector3 moveVector = transform.TransformDirection(playerMovementSpeed) * Speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (AllowInAirJump || character.isGrounded)
                {
                    ySpeed = JumpForce;
                    animator.SetTrigger("Jump");
                }
            }

            if (!character.isGrounded)
                ySpeed += Physics.gravity.y * ((character.velocity.y < 0) ? fallingMultiplier : 1) * Time.deltaTime;


            moveVector.y = ySpeed;
            character.Move(moveVector * Time.deltaTime);

            animator.SetBool("IsGrounded", character.isGrounded);
            animator.SetFloat("Speed", character.velocity.x / MaxSpeed);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (finished)
                return;
            
            if (collision.transform.name == "EndTrigger")
            {
                finished = true;
                PlayWinSequence();
                OnFinished?.Invoke();
            }
        }

        private void PlayWinSequence()
        {
            const float SlowDownTime = 0.5f;
            var seq = DOTween.Sequence();
            // slow down
            seq.Append(
                DOTween.To(() => Speed, (v) => Speed = v, 0, SlowDownTime).SetEase(Ease.InCirc));
            seq.Append(transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0, 180, 0), 0.3f));
        }

        private void PlayKilledSequence()
        {
            DOTween.Sequence()
                .AppendCallback(() => animator.SetTrigger("Kill"))
                .SetDelay(0.5f);
        }
    }
}