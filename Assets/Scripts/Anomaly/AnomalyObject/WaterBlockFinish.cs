using Cinemachine.PostFX;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Anomaly.Object
{
    public class WaterBlockFinish : MonoBehaviour // 마지막에 문 나갈때 vignette 정상적으로 풀어주는 코드
    {
        private CinemachineVolumeSettings CVS;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CVS = other.GetComponentInChildren<CinemachineVolumeSettings>();
                if (CVS != null && CVS.m_Profile.TryGet(out Vignette vignette))
                {
                    vignette.intensity.value = 0.2f;
                    vignette.color.value = Color.black;
                }
            }
        }
    }
}
