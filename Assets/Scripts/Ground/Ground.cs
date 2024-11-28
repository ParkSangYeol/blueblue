using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ground
{
    public class Ground : MonoBehaviour
    {
        public List<AudioClip> footStepSFXs;
        private int idx;

        private void Awake()
        {
            idx = 0;
        }

        public AudioClip GetFootStep()
        {
            idx++;
            idx %= footStepSFXs.Count;
            return footStepSFXs[idx];
        }
    }
}
