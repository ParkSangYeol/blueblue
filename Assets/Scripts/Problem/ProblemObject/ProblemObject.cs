using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Problem.Object
{
    public abstract class ProblemObject : MonoBehaviour
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