using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly.Object
{
    public class PhoneAddressDelete : SerializedMonoBehaviour
    {
        [Title("�޴��� ����ó ���� Ʈ����")]
        public bool triggerPhone = false;
        [Title("����ó UI�� �θ� Ojbect")]
        public Transform layoutGroup;
        [InfoBox("�� �ʸ��� 1���� ������� �� ���ΰ�")]
        public float deleteSec = 1.0f;

        private void Update()
        {
            if(triggerPhone)
            {
                triggerPhone = false;
                StartCoroutine(DisablePhoneAddress());
            }
        }

        IEnumerator DisablePhoneAddress()
        {
            for(int i = 0; i < layoutGroup.childCount; i++)
            {
                layoutGroup.GetChild(i).gameObject.SetActive(false);
                yield return new WaitForSeconds(deleteSec);
            }
        }
    }
}