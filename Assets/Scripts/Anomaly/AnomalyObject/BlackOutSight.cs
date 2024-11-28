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
        public float startDelay = 5f;
        public float blackoutTime = 2f;
        public Volume volumeProfile;

        private ColorAdjustments filter;

        void Start()
        {
            volumeProfile = FindObjectOfType<Volume>();
            if(volumeProfile.sharedProfile.TryGet(out filter))
            {
                filter.postExposure.value = 0f;
            }
            base.Start();
        }
        public override void ResetProblem()
        {
            filter.postExposure.value = 0f;
        }

        protected override void ActivePhenomenon()
        {
            StartCoroutine(Blackout(0f, -10f));
        }

        IEnumerator Blackout(float _start, float _end)
        {
            yield return new WaitForSeconds(startDelay);
            yield return StartCoroutine(ChangeExposure(_start, _end));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(ChangeExposure(_end, _start));
        }
        IEnumerator ChangeExposure(float _start, float _end)
        {
            Debug.Log("Start");
            float timeValue = 0f;
            while (timeValue < blackoutTime)
            {
                timeValue += Time.deltaTime;
                float t = timeValue / blackoutTime;

                filter.postExposure.value = Mathf.Lerp(_start, _end, t);
                yield return null;
            }
            filter.postExposure.value = _end;
        }
    }
}
