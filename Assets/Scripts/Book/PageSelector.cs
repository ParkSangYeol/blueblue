using System;
using System.Collections;
using System.Collections.Generic;
using Anomaly;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Book
{
    [RequireComponent(typeof(BookUI), typeof(BookSetter))]
    public class PageSelector : MonoBehaviour
    {
        [SerializeField] 
        private KeyCode leftKey;
        [SerializeField] 
        private KeyCode rightKey;

        private BookUI bookUI;
        private BookSetter bookSetter;

        private void Awake()
        {
            bookUI = GetComponent<BookUI>();
            bookSetter = GetComponent<BookSetter>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(leftKey))
            {
                MovePage(true);
            }

            if (Input.GetKeyDown(rightKey))
            {
                MovePage(false);
            }
        }

        private void MovePage(bool isLeft)
        {
            if (isLeft && bookUI.CurrentPage > 0)
            {
                bookUI.CurrentPage--;
            }
            
            if (!isLeft && bookUI.CurrentPage < bookSetter.numOfPage)
            {
                bookUI.CurrentPage++;
            }
        }
        
        public void SetPageToStartChapter(AnomalyType chapter)
        {
            int startPage = bookSetter.GetChapterStartPage(chapter);
            bookUI.CurrentPage = startPage != -1 ? startPage : 0;
        }
    }
}

