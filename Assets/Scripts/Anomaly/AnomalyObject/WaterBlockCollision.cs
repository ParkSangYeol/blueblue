using System.Collections;
using System.Collections.Generic;
using Cinemachine.PostFX;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Anomaly.Object
{
    public class WaterBlockCollision : AnomalyObject
    {
        [SerializeField] 
        private float deathTime = 5f;
        
        private CinemachineVolumeSettings CVS;
        

        private void Start()
        {
            base.Start();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CVS = other.GetComponentInChildren<CinemachineVolumeSettings>();
                ActivePhenomenon();
            }
        }

        protected override void ActivePhenomenon()
        {
            StartCoroutine(GetHurt());
        }

        private IEnumerator GetHurt()
        {

            if (CVS != null && CVS.m_Profile.TryGet(out Vignette vignette))
            {
                float elapsedTime = 0f;
                
                vignette.intensity.value = 0f;
                vignette.color.value = Color.red;

                while (elapsedTime < deathTime)
                {
                    vignette.intensity.value = Mathf.Lerp(0f, 1f, elapsedTime / deathTime);
                    elapsedTime += Time.deltaTime;

                    yield return null; 
                }
                
                Debug.Log("Dead");
                vignette.intensity.value = 1f;
            }
            else
            {
                Debug.LogWarning("No Vignette");
            }

            yield return null;
        }

        public override void ResetProblem()
        {
            //waterBlock.SetActive(false);
        }
    }

}