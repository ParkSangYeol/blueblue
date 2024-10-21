using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Problem
{
    [System.Flags]
    public enum ProblemType
    {
        BABYHOOD = 1 << 1,
        CHILDHOOD= 1 << 2,
        ADOLESCENCE = 1 << 3,
        OLD_AGE = 1 << 4
    }
}