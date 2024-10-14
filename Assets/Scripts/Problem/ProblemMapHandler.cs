using System;
using System.Collections;
using System.Collections.Generic;
using Problem.Object;
using UnityEngine;

namespace Problem
{
    public class ProblemMapHandler : MonoBehaviour
    {
        public Transform loadTransform;

        public DoorController leftDoor;
        public DoorController rightDoor;

        public NextProblemChoicer choiceTrueCollider;
        public NextProblemChoicer choiceFalseCollider;
        public ProblemUnLoader unloadCollider;
        
        public ProblemObject problem;

        public void ResetProblem()
        {
            // 이상 현상을 리셋
            Debug.Log("이상 현상 리셋");
        }
    }
}