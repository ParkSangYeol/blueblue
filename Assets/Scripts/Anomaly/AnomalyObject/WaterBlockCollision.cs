using System.Collections;
using System.Collections.Generic;
using Cinemachine.PostFX;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Anomaly.Object
{
    public class WaterBlockCollision : AnomalyObject
    {
        [SerializeField] private float increaseRate = 0.04f;
        [SerializeField] private float maxIntensity = 0.4f;
        
        private Vignette vignette;
        private CinemachineVolumeSettings CVS;
        private bool isPlayerCollide = false;
        private bool isGameOver = false;
        [SerializeField] private GameObject blackScreen;
        [SerializeField] private SFXPlayer sfxPlayer;
        [SerializeField] private AudioClip deadSFX;
        
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
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerCollide = false;
                Debug.Log("escape collision");
            }
        }
        
        protected override void ActivePhenomenon()
        {
           
            if (CVS != null && CVS.m_Profile.TryGet(out vignette))
            {
                if (vignette.color.value != Color.red)
                {
                    vignette.color.value = Color.red;
                }
                isPlayerCollide = true;
                StartCoroutine(GetHurt());
            }
            else
            {
                Debug.Log("Collide");
            }
        }

        private IEnumerator GetHurt()
        {
            while (isPlayerCollide && !isGameOver)
            {
                vignette.intensity.value = Mathf.Clamp(vignette.intensity.value + (increaseRate * Time.deltaTime), 0f,
                    maxIntensity);
                if (vignette.intensity.value >= maxIntensity)
                {
                    isGameOver = true;
                    Debug.Log("GameOver");
                    blackScreen.SetActive(true);
                    SoundManager.Instance.PlaySFX(sfxPlayer, deadSFX, false);
                    vignette.intensity.value = 0.2f;
                    vignette.color.value = Color.black;
                    yield return new WaitForSeconds(1.1f);
                    SceneManager.LoadScene(SceneManager.loadedSceneCount);
                }

                yield return null;  
            }
        }
        
        public override void ResetProblem()
        {
            blackScreen.SetActive(false);
        }
    }

}