using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Burst.CompilerServices;

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
        [Title("측면 판단 기준 각도")]
        [InfoBox("플레이어가 서있을 수 있는 발판의 최대 각도")]
        public float groundCheckAngle = 45f;
        [Title("Animator")]
        public Animator animator;

        //private Rigidbody rb;
        private CharacterController controller;
        private bool isGround = false;
        private bool overGroundAngle = false;

        private float _height;
        private float inputRL;
        private float inputFB;

        private Vector3 normalVector = Vector3.up;
        private Vector3 velocity;

        private void Start()
        {
            //rb = GetComponent<Rigidbody>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            //_height = GetComponent<CapsuleCollider>().height / 2 + 0.05f;
            _height = controller.height/2 + 0.05f;
            controller.slopeLimit = groundCheckAngle;
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
                normalVector = groundCheck.normal;//경사가 있는 발판이라면 경사의 법선 벡터 기록
                float angleCheck = Vector3.Angle(normalVector, Vector3.up);
                if (angleCheck < groundCheckAngle)
                {
                    isGround = true;
                    overGroundAngle = false;
                }
                else
                {
                    isGround = false;
                    overGroundAngle = true;
                }
            }
            else
            {
                isGround = false;
                overGroundAngle = false;
            }

            if(isGround && Input.GetKeyDown(KeyCode.Space))
            {
                //jump
                //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                velocity.y = jumpForce;
            }
            //animation
            animator.SetFloat("Speed", Mathf.Abs(inputFB) + Mathf.Abs(inputRL));
            animator.SetFloat("VelocityY", velocity.y);
            animator.SetBool("IsJumping", !isGround);
        }

        // walk 속도가 컴퓨터 사양에 따라 달라질 수 있으므로 FixedUpdate에서 처리할 수 있도록
        private void FixedUpdate()
        {   
            //walk
            Vector3 movement = new Vector3(inputRL, 0, inputFB).normalized;
            movement = transform.TransformDirection(movement);
            if (isGround && !overGroundAngle)// true,false
            {                
                movement *= moveSpeed;
                velocity = new Vector3(movement.x, velocity.y, movement.z);
            }
            else if(!isGround && !overGroundAngle)// false,false
            {
                movement *= atAirSpeed;
                velocity = new Vector3(movement.x, velocity.y, movement.z);
            }
            else
            {
                float angle = Vector3.Angle(normalVector, Vector3.up);
                Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, normalVector).normalized;
                velocity += slopeDirection * Physics.gravity.magnitude * Time.fixedDeltaTime;
            }
            //rb.AddForce(movement, ForceMode.Acceleration);
            //rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
            velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            if (isGround && velocity.y < 0) velocity.y = -3f;
        }
    } 
}
