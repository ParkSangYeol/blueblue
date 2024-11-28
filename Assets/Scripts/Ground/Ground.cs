using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ground
{
    public class Ground : MonoBehaviour
    {
        public AudioClip footStepSFX;

        public AudioClip GetFootStep()
        {
            return footStepSFX;
        }
    }
}
