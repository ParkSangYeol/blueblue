using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyClearData
{
    public Dictionary<string, bool> anomalyClearDictionary; // 이상현상 이름, 클리어 여부.

    public void AddData(string anomalyName)
    {
        anomalyClearDictionary.Add(anomalyName, false);    
    }
    
    public bool TryCheckData(string anomalyName)
    {
        if (anomalyClearDictionary.ContainsKey(anomalyName))
        {
            anomalyClearDictionary[anomalyName] = true;
            return true;
        }

        return false;
    }
    
    public AnomalyClearData()
    {
        anomalyClearDictionary = new Dictionary<string, bool>();
    }
}