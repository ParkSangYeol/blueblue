using System.Collections;
using System.Collections.Generic;
using Anomaly;
using Book;
using UnityEngine;
using UnityEngine.UI.Extensions;
using PageSelector = Book.PageSelector;

namespace System.Interaction
{
    public class BookInteractive : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private PageSelector pageSelector;

        [SerializeField] 
        private AnomalyType anomalyType;
        
        public void StartInteract()
        {
            pageSelector.gameObject.SetActive(true);
            pageSelector.SetPageToStartChapter(anomalyType);
        }

        public void StopInteract()
        {
            pageSelector.gameObject.SetActive(false);
        }
    }
}
