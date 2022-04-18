using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace OneDay.Games.Jumper
{
    public class RBCharacter : MonoBehaviour
    {
        [SerializeField] private float Speed;
        [SerializeField] private float JumpForce;
        [SerializeField] private Vector3 playerMovementSpeed;

        [Range(0, 3)] [SerializeField] private float fallingMultiplier = 1;

        private Rigidbody rb;
        private Animator animator;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            playerMovementSpeed = new Vector3(0, 0, Input.GetAxis("Horizontal"));

            MovePlayer();
        }

        private void MovePlayer()
        {
            Vector3 moveVector = transform.TransformDirection(playerMovementSpeed) * Speed;
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

                animator.SetTrigger("Jump");
            }

            // falling
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * fallingMultiplier * Time.deltaTime;
            }

            animator.SetFloat("Speed", playerMovementSpeed.z);
            animator.SetFloat("JumpSpeed", Math.Abs(rb.velocity.y));
        }


        private void OnCollisionEnter(Collision collision)
        {
            animator.SetBool("TouchingGround", true);
        }

        private void OnCollisionExit(Collision collision)
        {
            animator.SetBool("TouchingGround", false);
        }
    }
}
