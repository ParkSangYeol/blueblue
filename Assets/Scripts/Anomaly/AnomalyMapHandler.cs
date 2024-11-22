using System;
using System.Collections;
using System.Collections.Generic;
using Anomaly.Object;
using UnityEngine;

namespace Anomaly
{
    public class AnomalyMapHandler : MonoBehaviour
    {
        public Transform loadTransform;

        public DoorController leftDoor;
        public DoorController rightDoor;

        public NextAnomalyChoicer choiceTrueCollider;
        public NextAnomalyChoicer choiceFalseCollider;
        public AnomalyUnLoader unloadCollider;
        
        public AnomalyObject problem;

        public void ResetProblem()
        {
            // 이상 현상을 리셋
            Debug.Log("이상 현상 리셋");
        }
    }
}