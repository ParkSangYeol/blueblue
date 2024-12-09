using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace PlayerControl
{
    public class RotateCamera : SerializedMonoBehaviour
    {
        [Title("�� �ڵ�� ī�޶� �޾��ּ���.")]
        [DetailedInfoBox("ī�޶� ����� ��ü", "������ ���� ����, ĳ������ �� �κп� �����ϼ���.")]
        public Transform playerHead;
        [InfoBox("���콺 ����")]
        public float mouseSensitivity = 100f;
        //public float mouseSensitivityVertical = 100f;
        //public float mouseSensitivityHorizontal = 100f;
        private float xRotation = 0f;
        [InfoBox("���콺 ���� ���� ����ġ ����")]
        [MinMaxSlider(-180, 180,true)]
        public Vector2 mouse_Up_Down_Restrict = new Vector2(-90, 90);

        private bool jumpScareTriggered = false;
        void Start()
        {
            mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
            //mouseSensitivityHorizontal = PlayerPrefs.GetFloat("SensitivityX", 100f);
            //mouseSensitivityVertical = PlayerPrefs.GetFloat("SensitivityY", 100f);
            SettingUIManager.Instance.onSensitivityValueChanged.AddListener(SetSensitivity);
            //SettingUIManager.Instance.onSensitivityValueChanged.AddListener(SetSensitivityX);
            //SettingUIManager.Instance.onSensitivityValueChanged.AddListener(SetSensitivityY);
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityHorizontal * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            //float mouseY = Input. GetAxis("Mouse Y") * mouseSensitivityVertical * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, mouse_Up_Down_Restrict.x, mouse_Up_Down_Restrict.y);

            if (!jumpScareTriggered)
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerHead.Rotate(Vector3.up * mouseX);
            }
        }

        private void SetSensitivity()
        {
            mouseSensitivity = SettingUIManager.Instance.GetSensitivity();
            //mouseSensitivityHorizontal = SettingUIManager.Instance.GetSensitivityX();
            //mouseSensitivityVertical = SettingUIManager.Instance.GetSensitivityY();
        }

        public void WhenJumpScareTriggered()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            jumpScareTriggered = true;
        }
    }
}
