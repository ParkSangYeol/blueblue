using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Burst.CompilerServices;

namespace PlayerControl {
    public class PlayerController : SerializedMonoBehaviour
    {
        [Title("�̵� ���� ����")]
        [InfoBox("�ȴ� �ӵ�")]
        public float moveSpeed = 5f;
        [DetailedInfoBox("���� �̵��� �ӵ�", "Jump ���¿��� WASD���� �� �������� �����̴� �ӵ�")]
        public float atAirSpeed = 3;
        [InfoBox("������ �������� ��")]
        public float jumpForce = 7f;
        [InfoBox("�������� �ְ� �ӵ�")]
        public float highestFallSpeed = -3f;
        [InfoBox("�������� �ӵ� ���� ������")]
        public float fallSpeedFilter = 1f;
        [Title("Ground Layer")]
        [InfoBox("�� �־��ּ���")]
        public LayerMask ground;
        [InfoBox("�÷��̾ õ�忡�� �΋H�� �� �ִ� ������Ʈ")]
        public string groundTagName;
        [Title("���� �Ǵ� ���� ����")]
        [InfoBox("�÷��̾ ������ �� �ִ� ������ �ִ� ����")]
        public float groundCheckAngle = 45f;
        [Title("Animator")]
        public Animator animator;
        [Title("GroundRay")]
        [InfoBox("�ٴ��� �����ϱ� ���� ��� ������ ����(0.2f ���� scale 0.5 ~ 1.0���� ���������� �۵���")]
        public float groundRay = 0.2f;

        //private Rigidbody rb;
        private CharacterController controller;
        private bool isGround = false;
        private bool overGroundAngle = false;

        private float _height;
        private float inputRL;
        private float inputFB;

        private Vector3 normalVector = Vector3.up;
        private Vector3 velocity;

        private float sphereRadius = 0.1f;

        private void Start()
        {
            //rb = GetComponent<Rigidbody>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            //_height = GetComponent<CapsuleCollider>().height / 2 + 0.05f;
            _height = controller.height/2 + 0.03f;
            controller.slopeLimit = groundCheckAngle;
            sphereRadius *= transform.localScale.y;
        }

        private void Update()
        {
            // WASD�� �Է��� �޾ƿ���
            inputRL = Input.GetAxis("Horizontal"); // Right - Left (A - D)
            inputFB = Input.GetAxis("Vertical"); // Front - Back (W - S)

            //ground check
            RaycastHit groundCheck;
            //Vector3 rayPos = transform.position + Vector3.up*_height;
            Debug.DrawRay(transform.position, Vector3.down*groundRay, Color.red);
            if (Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out groundCheck, groundRay, ground))
            {
                normalVector = groundCheck.normal;//��簡 �ִ� �����̶�� ����� ���� ���� ���
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

            //RaycastHit ceilingCheck;
            //Vector3 fRayPos = transform.position + Vector3.up * 0.05f;
            //if (Physics.Raycast(fRayPos, Vector3.up, out ceilingCheck, _height, ground))
            //{
            //    Debug.Log("Ceiling");
            //    isGround = false;
            //    velocity.y = highestFallSpeed;
            //}

                if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                //jump
                //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                velocity.y = jumpForce;
                isGround = false;
            }
            //animation
            animator.SetFloat("Speed", Mathf.Abs(inputFB) + Mathf.Abs(inputRL));
            animator.SetFloat("VelocityY", velocity.y);
            animator.SetBool("IsJumping", !isGround);
        }

        // walk �ӵ��� ��ǻ�� ��翡 ���� �޶��� �� �����Ƿ� FixedUpdate���� ó���� �� �ֵ���
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
                velocity += slopeDirection * Physics.gravity.magnitude * Time.fixedDeltaTime * fallSpeedFilter;
            }
            //rb.AddForce(movement, ForceMode.Acceleration);
            //rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
            velocity.y += Physics.gravity.y * Time.deltaTime * fallSpeedFilter;
            controller.Move(velocity * Time.deltaTime);
            if (isGround && velocity.y < 0) velocity.y = highestFallSpeed;
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.CompareTag(groundTagName))
            {
                velocity.y = 0;
            }
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    // Trigger �Ǵ� �̻������� ���⿡�� ����ϸ� �˴ϴ�.
        //}
    }
}
