using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Problem.Object
{
    public class WindowOpeningProblem : ProblemObject
    {
        [SerializeField]
        private DOTweenAnimation animation;

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
            animation.DORestart();
            OnWindowOpening.Invoke();
        }

        public override void ResetProblem()
        {
            animation.DORewind();
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