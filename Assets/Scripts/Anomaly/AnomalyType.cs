using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly
{
    [System.Flags]
    public enum AnomalyType
    {
        BABYHOOD = 1 << 1,
        CHILDHOOD= 1 << 2,
        ADOLESCENCE = 1 << 3,
        OLD_AGE = 1 << 4
    }
}