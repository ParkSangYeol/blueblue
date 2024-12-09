using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Anomaly.Object
{
    public class PhoneAddressDelete : AnomalyObject
    {
        public Transform layoutGroup;
        public float deleteSec = 1.0f;

        void Start()
        {
            base.Start();
        }

        IEnumerator DisablePhoneAddress()
        {
            for(int i = 0; i < layoutGroup.childCount; i++)
            {
                layoutGroup.GetChild(i).gameObject.SetActive(false);
                yield return new WaitForSeconds(deleteSec);
            }
        }
        public void CallActivePhenomenon()
        {
            ActivePhenomenon();
        }

        protected override void ActivePhenomenon()
        {
            StartCoroutine(DisablePhoneAddress());
        }

        public override void ResetProblem()
        {
        }
    }
}