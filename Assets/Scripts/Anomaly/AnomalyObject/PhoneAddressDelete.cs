using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly.Object
{
    public class PhoneAddressDelete : SerializedMonoBehaviour
    {
        [Title("휴대폰 연락처 삭제 트리거")]
        public bool triggerPhone = false;
        [Title("연락처 UI의 부모 Ojbect")]
        public Transform layoutGroup;
        [InfoBox("몇 초마다 1개씩 사라지게 할 것인가")]
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