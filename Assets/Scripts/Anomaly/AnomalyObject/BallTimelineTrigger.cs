using Anomaly.Object;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

namespace Anomaly.Object
{
    public class BallTimelineTrigger : AnomalyObject
    {
        public PlayableDirector pd;
        public GameObject ball;

        private Rigidbody rb;
        private bool isTrigger = false;
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        public AudioClip ballSFX;

        public override void ResetProblem()
        {
            if(rb != null)
            {
                rb.isKinematic = true;
                rb.position = initialPosition;
                rb.rotation = initialRotation;
                rb.isKinematic = false;
            }

            isTrigger = true;
        }

        protected override void ActivePhenomenon()
        {
            if (rb != null) rb.isKinematic = true;
            pd.Play();
            if(ballSFX != null)
            {
                SoundManager.Instance.PlaySFX(ballSFX);
            }
            if (rb != null) StartCoroutine(WaitEndTimeline());
        }

        private void Start()
        {
            base.Start();
            rb = ball.GetComponent<Rigidbody>();

            if (rb != null)
            {
                initialPosition = rb.position; // 초기 위치 저장
                initialRotation = rb.rotation; // 초기 회전 저장
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isTrigger)
            {
                Debug.Log("Start_ball");
                isTrigger = true;
                ActivePhenomenon();
            }
        }

        IEnumerator WaitEndTimeline()
        {
            yield return new WaitForSeconds((float)pd.duration);
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
}
