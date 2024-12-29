using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Anomaly.Object
{
    public class WindowOpeningProblem : AnomalyObject
    {
        [SerializeField]
        private DOTweenAnimation animation;
        
        [SerializeField] 
        private SFXPlayer sfxPlayer;
        
        [SerializeField] 
        private AudioClip openingSFX;

        public UnityEvent OnWindowOpening;

        private void Awake()
        {
            if (animation == null)
            {
                Debug.LogError("창문 DOTween Animation이 지정되지 않았습니다.");
            }
        }
        
        protected override void ActivePhenomenon()
        {
            StartCoroutine(OpenWindow());
        }

        public override void ResetProblem()
        {
            animation.DORewind();
        }

        IEnumerator OpenWindow()
        {
            SoundManager.Instance.PlaySFX(sfxPlayer, openingSFX);
            yield return new WaitForSeconds(0.1f);
            animation.DORestart();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ActivePhenomenon();
            }
        }
    }
}