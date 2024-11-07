using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Interaction
{
    // 상호작용이 가능한 오브젝트가 상속받는 인터페이스.
    public interface IInteractable
    {
        /// <summary>
        /// 상호작용을 시작함.
        /// 예를 들어 스마트폰의 경우 상호작용시 스마트폰 화면이 팝업으로 뜸.
        /// </summary>
        public void StartInteract();
        
        /// <summary>
        /// 상호작용을 종료함.
        /// 이 함수의 결과로 다시 StartInteract를 호출할 때 문제가 없음을 보장해야함.
        /// 예를 들어 스마트폰의 경우 상호작용 종료시 팝업 된 스마트폰 화면이 내려감.
        /// </summary>
        public void StopInteract();
    }

}