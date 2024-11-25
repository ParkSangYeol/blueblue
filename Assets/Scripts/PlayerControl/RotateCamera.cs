using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class RotateCamera : SerializedMonoBehaviour
    {
        [Title("�� �ڵ�� ī�޶� �޾��ּ���.")]
        [DetailedInfoBox("ī�޶� ����� ��ü", "������ ���� ����, ĳ������ �� �κп� �����ϼ���.")]
        public Transform playerHead;
        //private Rigidbody rb;
        [InfoBox("���콺 ����")]
        public float mouseSensitivity = 100f;
        private float xRotation = 0f;
        [InfoBox("���콺 ���� ���� ����ġ ����")]
        [MinMaxSlider(-180, 180,true)]
        public Vector2 mouse_Up_Down_Restrict = new Vector2(-90, 90);

        private bool jumpScareTriggered = false;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            //rb = playerHead.GetComponent<Rigidbody>();
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, mouse_Up_Down_Restrict.x, mouse_Up_Down_Restrict.y);

            if (!jumpScareTriggered)
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                //playerHead.Rotate(Vector3.up * mouseX);
                // ĳ���Ϳ� rigidbody�� �浹�� �־ �߻��� ����

                //Quaternion yRotation = Quaternion.Euler(0f, mouseX, 0f);
                //rb.MoveRotation(rb.rotation * yRotation);
                playerHead.Rotate(Vector3.up * mouseX);
            }
        }

        public void WhenJumpScareTriggered()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            jumpScareTriggered = true;
        }
    }
}
