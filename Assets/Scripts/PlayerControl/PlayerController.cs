using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Burst.CompilerServices;
using ground;

namespace PlayerControl {
    public class PlayerController : SerializedMonoBehaviour
    {
        [Title("�̵� ���� ����")]
        [InfoBox("�ȴ� �ӵ�")]
        public float moveSpeed = 5f;
        [InfoBox("���� ����: ������ �� ������ �پ�鵵��")]
        public float deceleration = 8f;
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
        [InfoBox("�Ҹ� ��� �� - ���� Ŭ�� �ð��� ���缭")]
        public float maxFootStepTimer = 1f;

        private float footStepTimer;

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

        private bool jumpScareTriggered = false;


        private void Start()
        {
            //rb = GetComponent<Rigidbody>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            //_height = GetComponent<CapsuleCollider>().height / 2 + 0.05f;
            _height = controller.height*transform.localScale.y;
            controller.slopeLimit = groundCheckAngle;
            sphereRadius *= transform.localScale.y;
            Collider characterCollider = controller.GetComponent<Collider>();
            if (characterCollider != null)
            {
                Physics.IgnoreCollision(characterCollider, characterCollider, true);
            }
            footStepTimer = 0.05f;
        }

        private void Update()
        {
            // WASD�� �Է��� �޾ƿ���
            inputRL = Input.GetAxisRaw("Horizontal"); // Right - Left (A - D)
            inputFB = Input.GetAxisRaw("Vertical"); // Front - Back (W - S)

            ////ground check
            //RaycastHit groundCheck;
            ////Vector3 rayPos = transform.position + Vector3.up*_height;
            //Debug.DrawRay(transform.position, Vector3.down*groundRay, Color.red);
            //Debug.DrawRay(transform.position + Vector3.up * _height, Vector3.up * groundRay, Color.blue);
            //if (Physics.SphereCast(transform.position, groundRay, Vector3.down, out groundCheck, 0f, ground))
            //{
            //    Debug.Log($"Hit: {groundCheck.collider.name}");
            //    normalVector = groundCheck.normal;//��簡 �ִ� �����̶�� ����� ���� ���� ���
            //    float angleCheck = Vector3.Angle(normalVector, Vector3.up);
            //    if (angleCheck < groundCheckAngle)
            //    {
            //        isGround = true;
            //        overGroundAngle = false;
            //    }
            //    else
            //    {
            //        isGround = false;
            //        overGroundAngle = true;
            //    }
            //}
            //else
            //{
            //    isGround = false;
            //    overGroundAngle = false;
            //}

            RaycastHit[] groundChecks;
            Debug.DrawRay(transform.position, Vector3.down * groundRay, Color.red);
            Debug.DrawRay(transform.position + Vector3.up * _height, Vector3.up * groundRay, Color.blue);
            groundChecks = Physics.SphereCastAll(
                    transform.position,    // ���� ��ġ
                    groundRay,          // ���� ������
                    Vector3.down,          // ����
                    0.05f,             // �ִ� �Ÿ�
                    ground                 // ���̾� ����ũ
                    );
            isGround = false;
            overGroundAngle = false;

            foreach (RaycastHit hit in groundChecks)
            {
                // �ڽ��̳� �����ؾ� �� �浹ü�� ���͸�
                if (hit.collider.gameObject != gameObject)
                {
                    //Debug.Log($"Hit: {hit.collider.name}");
                    normalVector = hit.normal; // ��� ���� ���� ���
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
                    break; // ���� ����� �浹�� ó��
                }
            }

            // �浹�� ���� ��� �⺻������ ����
            if (!isGround)
            {
                isGround = false;
                overGroundAngle = false;
            }

            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                //jump
                //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                velocity.y = jumpForce;
                isGround = false;
            }
            CheckFootStep();
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

            if (inputRL == 0 && inputFB == 0 && isGround)
            {
                velocity.x = 0;
                velocity.z = 0;
            }
            if(velocity.y > highestFallSpeed)
                velocity.y += Physics.gravity.y * Time.deltaTime * fallSpeedFilter;
            if(!jumpScareTriggered)
                controller.Move(velocity * Time.deltaTime);
            if (isGround && velocity.y < 0) velocity.y = highestFallSpeed;
        }

        private AudioClip GetFootStepSFX()
        {
            AudioClip result = null;
            RaycastHit[] groundChecks = Physics.SphereCastAll(
                    transform.position,    // ���� ��ġ
                    groundRay,          // ���� ������
                    Vector3.down,          // ����
                    0.05f,             // �ִ� �Ÿ�
                    ground                 // ���̾� ����ũ
                    );

            foreach (RaycastHit hit in groundChecks)
            {
                GameObject g = hit.collider.gameObject;
                // �ڽ��̳� �����ؾ� �� �浹ü�� ���͸�
                if (g.TryGetComponent<Ground>(out var groundComponent))
                {
                    result = groundComponent.GetFootStep();
                }
            }

            return result;
        }

        private void CheckFootStep()
        {
            AnimatorStateInfo a = animator.GetCurrentAnimatorStateInfo(0);

            if(a.IsName("Base Layer.Walking"))
            {
                footStepTimer -= Time.deltaTime;
            }

            if(footStepTimer > 0f)
            {
                return;
            }
            AudioClip footStepSFX = GetFootStepSFX();
            if (footStepSFX != null)
            {
                SoundManager.Instance.PlaySFX(footStepSFX);
                footStepTimer = maxFootStepTimer;
            }
        }

        public void WhenJumpScareTriggered()
        {
            jumpScareTriggered= true;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.CompareTag(groundTagName) && velocity.y > 0)
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
