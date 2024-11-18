using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly.Object
{
   public class WaterRise : AnomalyObject
   {
      
      public GameObject waterBlock;
      
      [Tooltip("올라오는 속도 : 1.0 ~ 10.0 설정")]
      public float waterRiseSpeed;

      private void Start()
      {
         base.Start();
         waterBlock.SetActive(false);
      }

      private void OnTriggerEnter(Collider other)
      {
         if (other.CompareTag("Player"))
         {
            ActivePhenomenon();
         }
      }

      protected override void ActivePhenomenon()
      {
         waterBlock.SetActive(true);
         StartCoroutine(RiseWater());
      }

      private IEnumerator RiseWater()
      {
         while (waterBlock.transform.localPosition.y < 80f) 
         {
            waterBlock.transform.localPosition += new Vector3(0, waterRiseSpeed * Time.deltaTime, 0);
            yield return null;  
         }
         waterBlock.SetActive(false);
      }

      public override void ResetProblem()
      {
         waterBlock.SetActive(false);
      }
   }
}
 

