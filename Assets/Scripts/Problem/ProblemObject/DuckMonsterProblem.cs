using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Problem.Object
{
    public class DuckMonsterProblem : ProblemObject
    {
        private Vector3 startPos;
        private NavMeshAgent agent;
        private Transform playerTransform;
        private bool isTracking;
        
        public UnityEvent onCatchPlayer;
        
        private void Awake()
        {
            startPos = transform.position;
            agent = GetComponent<NavMeshAgent>();
            onCatchPlayer.AddListener(() =>
            {
                isTracking = false;
            });
        }

        private void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            
            base.Start();
        }
        
        [Button]
        protected override void ActivePhenomenon()
        {
            StartCoroutine(TrackPlayer());
        }

        public override void ResetProblem()
        {
            transform.position = startPos;
        }

        IEnumerator TrackPlayer()
        {
            isTracking = true;
            Debug.Log("PlayerPos is " + playerTransform.position);
            while (isTracking)
            {
                agent.SetDestination(playerTransform.position);
                Debug.Log("PlayerPos is " + playerTransform.position);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // 플레이어 잡음
                onCatchPlayer.Invoke();
            }
        }
    }
}
