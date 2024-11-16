using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Anomaly.Object
{
    public abstract class AnomalyObject : MonoBehaviour
    {
        public bool activeOnStart;
        protected abstract void ActivePhenomenon();
        public abstract void ResetProblem();

        protected void Start()
        {
            if (activeOnStart)
            {
                ActivePhenomenon();
            }
        }
    }
}