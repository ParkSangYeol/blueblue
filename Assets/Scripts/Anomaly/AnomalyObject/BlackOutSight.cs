using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Anomaly.Object
{
    public class BlackOutSight : AnomalyObject
    {
        public float blackoutTime = 4f;
        public Volume volumeProfile;
        private bool isTriggered = false;

        private Vignette vignette;

        void Start()
        {
            volumeProfile = FindObjectOfType<Volume>();
            if(volumeProfile.sharedProfile.TryGet(out vignette))
            {
                vignette.intensity.value = 0.2f;
            }
            base.Start();
        }
        public override void ResetProblem()
        {
            StartCoroutine(ChangeVignette(0.5f, 0.2f));
            isTriggered = false;
            GetComponentInChildren<EndBlackOut1>().ResetAnomaly();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isTriggered)
            {
                isTriggered = true;
                ActivePhenomenon();
            }
        }
        protected override void ActivePhenomenon()
        {
            StartCoroutine(ChangeVignette(0.2f, 0.5f));
        }

        IEnumerator ChangeVignette(float _start, float _end)
        {
            Debug.Log("Start");
            float timeValue = 0f;
            while (timeValue < blackoutTime)
            {
                timeValue += Time.deltaTime;
                float t = timeValue / blackoutTime;

                vignette.intensity.value = Mathf.Lerp(_start, _end, t);
                yield return null;
            }
            vignette.intensity.value = _end;
        }
    }
}
