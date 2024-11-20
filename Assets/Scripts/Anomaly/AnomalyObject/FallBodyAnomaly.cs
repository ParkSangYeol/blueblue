using System;
using System.Collections;
using System.Collections.Generic;
using Anomaly.Object;
using DG.Tweening;
using UnityEngine;

public class FallBodyAnomaly : AnomalyObject
{
    private bool isActive;
    [SerializeField]
    private DOTweenAnimation doTweenAnimation;

    [SerializeField] 
    private SFXPlayer brokeSfxPlayer;
    
    [SerializeField]
    private AudioClip windowBroke;
    [SerializeField] 
    private AudioClip bodyFall;

    private void Awake()
    {
        isActive = false;
    }

    protected override void ActivePhenomenon()
    {
        isActive = true;
        StartCoroutine(FallProduction());
    }

    public override void ResetProblem()
    {
        isActive = false;
        doTweenAnimation.DORewind();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.CompareTag("Player"))
        {
            ActivePhenomenon();
        }
    }

    IEnumerator FallProduction()
    {
        SoundManager.Instance.PlaySFX(brokeSfxPlayer, windowBroke);
        yield return new WaitForSeconds(1f);
        doTweenAnimation.DORestart();
        SoundManager.Instance.PlaySFX(bodyFall);
    }
}
