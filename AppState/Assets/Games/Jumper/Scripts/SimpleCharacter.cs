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
        [SerializeField] private float groundRayLength = 1.0f;
        [Range(0, 3)] [SerializeField] private float fallingMultiplier = 1;

        private Animator animator;
        private float ySpeed;
        private bool running;
        private bool finished;


        private Jumper inJumper;
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
            bool isGrounded = IsGrounded();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (AllowInAirJump || isGrounded)
                {
                    ySpeed = JumpForce;
                    animator.SetTrigger("Jump");
                    Speed *= Mathf.Max(character.velocity.x / MaxSpeed, 0.9f);
                }
            }

            if (!isGrounded)
            {
                ySpeed += Physics.gravity.y * ((character.velocity.y < 0) ? fallingMultiplier : 1) * Time.deltaTime;
            }
            else if (!finished)
            {
                float acceleration = 2.0f;
                Speed = Mathf.Lerp(Speed, MaxSpeed, Time.deltaTime * acceleration);
            }
            
            moveVector.y = ySpeed;
            character.Move(moveVector * Time.deltaTime);

            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("Speed", character.velocity.x / MaxSpeed);
            animator.SetFloat("VerticalSpeed", character.velocity.y);

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
            else if (collision.transform.name == "JumperMain")
            {
                inJumper = collision.GetComponent<Jumper>();
                Debug.Log("In jumper");
                Time.timeScale = 0.5f;
            }
            
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.transform.name == "JumperMain")
            {
                inJumper = null;
                Debug.Log("Out of jumper");
                Time.timeScale = 1.0f;

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

        private bool IsGrounded()
        {
            
            var hits = Physics.RaycastAll(new Ray(transform.position, Vector3.down), groundRayLength);
            foreach (var hit in hits)
            {
                if (hit.collider.name != "Jumper" && hit.collider.name != "JumperMain")
                {
                    return true;
                }
            }

            return false;
            //if (Physics.RaycastAll(new Ray(transform.position, Vector3.down), groundRayLength))
            //{
            //    return true;
            //}

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.down * groundRayLength);
        }
    }
}