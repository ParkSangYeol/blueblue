using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Problem.Object
{
    public class SchoolBellProblem : ProblemObject
    {
        [SerializeField] 
        private AudioClip audioClip;

        [SerializeField] 
        private SFXPlayer sfxPlayer;

        private bool isActive = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isActive)
            {
                Debug.Log("Active!");
                isActive = true;
                ActivePhenomenon();
            }
        }

        protected override void ActivePhenomenon()
        {
            SoundManager.Instance.PlaySFX(sfxPlayer, audioClip, false);
        }

        public override void ResetProblem()
        {
            isActive = false;
            sfxPlayer.Stop();
        }
    }
}