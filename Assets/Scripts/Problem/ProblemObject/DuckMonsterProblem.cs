using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
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

        [SerializeField] 
        private GameObject jumpScareDuck;

        [SerializeField] 
        private Camera mainCamera;
        
        [SerializeField] 
        private SFXPlayer sfxPlayer;
        
        [SerializeField] 
        private AudioClip screamSFX;
        
        public UnityEvent onCatchPlayer;
        private Sequence jumpScareSequence;
        private void Awake()
        {
            startPos = transform.position;
            agent = GetComponent<NavMeshAgent>();
            onCatchPlayer.AddListener(() =>
            {
                isTracking = false;
            });
            onCatchPlayer.AddListener(() =>
            {
                StartCoroutine(JumpScare());
            });
        }

        private void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            
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
            if (isTracking && other.CompareTag("Player"))
            {
                // 플레이어 잡음
                onCatchPlayer.Invoke();
            }
        }

        IEnumerator JumpScare()
        {
            Debug.Log("Call");
            // TODO 플레이어 이동 제한 및 카메라 움직임 제한시키기
            // TODO 게임 내 모든 조명 비활성화하기
            // 괴물 오리 오브젝트 이동
            jumpScareDuck.transform.position = playerTransform.position + new Vector3(0, 2f, 0) + playerTransform.forward * 0.26f;
            jumpScareDuck.transform.rotation = mainCamera.transform.rotation;
            // jumpScareDuck.transform.rotation = new Quaternion(0f, jumpScareDuck.transform.rotation.y, 0f, 1f);
            jumpScareDuck.transform.Rotate(345,180,0,Space.Self);
            yield return null;
            // 점프스케어 연출 진행
            jumpScareSequence = DOTween.Sequence();
            jumpScareSequence.SetAutoKill(false);
            jumpScareSequence
                .Append(jumpScareDuck.transform
                    .DOMoveY(mainCamera.transform.position.y - 0.17f, 0.5f).SetEase(Ease.OutExpo))
                .Append(jumpScareDuck.transform
                    .DORotateQuaternion(playerTransform.rotation * Quaternion.Euler(15, 180, 0), 0.3f)
                    .SetDelay(1f));
            // TODO 카메라 흔들림 연출. 현재 CineMachine Brain을 사용해 DOTween 연출이 안먹음. 
            // SFX 출력
            SoundManager.Instance.PlaySFX(sfxPlayer, screamSFX, false);
            yield return null;
            // TODO 화면 암전 게임 오버 연출 진행.
        }
    }
}
