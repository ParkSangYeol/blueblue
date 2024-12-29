using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Book
{
    public class PageSetter : MonoBehaviour
    {
        [SerializeField] 
        private Image image;

        [SerializeField] 
        private Text text;

        [SerializeField] 
        private GameObject inactiveCover;
        /// <summary>
        /// PageData를 기반으로 page 정보를 갱신
        /// </summary>
        /// <param name="pageData"></param>
        public void InitPage(PageScriptableObject pageData)
        {
            image.sprite = pageData.photo;
            text.text = pageData.description;
        }

        /// <summary>
        /// 기본으로 비활성화 된 Page를 활성화시킴.
        /// </summary>
        public void SetActivate()
        {
            inactiveCover.SetActive(false);
        }
    }
}
