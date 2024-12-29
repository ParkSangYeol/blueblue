using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anomaly.Object
{
   public class WaterRise : AnomalyObject
   {
      
      [SerializeField] private GameObject waterBlock;
      [SerializeField] private SFXPlayer sfxPlayer;
      [SerializeField] private AudioClip waterSFX;
      
      
      [Tooltip("올라오는 속도 : 1.0 ~ 15.0 설정")]
      public float maxWaterRiseSpeed;
      
      [Tooltip("올라오는 가속도: 낮을수록 천천히, 높을수록 빠르게")]
      public float accelerationFactor = 0.5f;

      private float currentSpeed = 0f; 
      
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
         SoundManager.Instance.PlaySFX(sfxPlayer, waterSFX, true);
         StartCoroutine(RiseWater());
      }

      private IEnumerator RiseWater()
      {
         Vector3 startPosition = waterBlock.transform.localPosition;
         Vector3 targetPosition = new Vector3(startPosition.x, 52f, startPosition.z);

         float elapsedTime = 0f; 

         while (waterBlock.transform.localPosition.y < targetPosition.y)
         {
            
            currentSpeed = Mathf.Lerp(0f, maxWaterRiseSpeed, elapsedTime * accelerationFactor);
            waterBlock.transform.localPosition += new Vector3(0, currentSpeed * Time.deltaTime, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
         }
         
         waterBlock.transform.localPosition = targetPosition;
      }

      public override void ResetProblem()
      {
         waterBlock.SetActive(false);
         currentSpeed = 0f;
      }
   }
}
 

