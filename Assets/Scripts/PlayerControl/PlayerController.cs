using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PlayerControl {
    public class PlayerController : SerializedMonoBehaviour
    {
        [Title("이동 관련 변수")]
        [InfoBox("걷는 속도")]
        public float moveSpeed = 5f;
        [DetailedInfoBox("공중 이동시 속도", "Jump 상태에서 WASD사용시 각 방향으로 움직이는 속도")]
        public float atAirSpeed = 3;
        [InfoBox("점프시 가해지는 힘")]
        public float jumpForce = 7f;
        [Title("Ground Layer")]
        [InfoBox("잘 넣어주세요")]
        public LayerMask ground;
        [Title("Animator")]
        public Animator animator;

        private Rigidbody rb;
        private bool isGround = false;

        private float _height;
        private float inputRL;
        private float inputFB;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            _height = GetComponent<CapsuleCollider>().height / 2 + 0.05f;
        }

        private void Update()
        {
            // WASD로 입력을 받아오고
            inputRL = Input.GetAxis("Horizontal"); // Right - Left (A - D)
            inputFB = Input.GetAxis("Vertical"); // Front - Back (W - S)

            //ground check
            RaycastHit groundCheck;
            if (Physics.Raycast(transform.position, Vector3.down, out groundCheck, _height, ground))
            {
                isGround = true;
            }
            else
            {
                isGround = false;
            }

            if(isGround && Input.GetKeyDown(KeyCode.Space))
            {
                //jump
                //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            //animation
            animator.SetFloat("Speed", Mathf.Abs(inputFB) + Mathf.Abs(inputRL));
            animator.SetFloat("VelocityY", rb.velocity.y);
            animator.SetBool("IsJumping", !isGround);
        }

        // walk 속도가 컴퓨터 사양에 따라 달라질 수 있으므로 FixedUpdate에서 처리할 수 있도록
        private void FixedUpdate()
        {   
            //walk
            Vector3 movement = new Vector3(inputRL, 0, inputFB).normalized;
            movement = transform.TransformDirection(movement);
            if (isGround)
            {
                movement *= moveSpeed;

            }
            else
            {
                movement *= atAirSpeed;
            }
            //rb.AddForce(movement, ForceMode.Acceleration);
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }
    } 
}
