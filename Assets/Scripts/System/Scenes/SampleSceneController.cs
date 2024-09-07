using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System.Scenes
{
    public class SampleSceneController : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField;
        
        void Start()
        {
            if (inputField == null)
            {
                inputField = GameObject.Find("IF_SceneName").GetComponent<TMP_InputField>();
            }
        }

        public void AddLeftScene()
        {
            if (SceneController.Instance == null)
            {
                Debug.LogError("SceneController가 없습니다.");
                return;
            }
            SceneController.Instance.AddScene(inputField.text, SceneAddType.LEFT);
        }

        public void AddRightScene()
        {
            SceneController.Instance.AddScene(inputField.text, SceneAddType.RIGHT);
        }

        public void RemoveLeftScene()
        {
            SceneController.Instance.RemoveScene(SceneAddType.LEFT);
        }

        public void RemoveRightScene()
        {
            SceneController.Instance.RemoveScene(SceneAddType.RIGHT);
        }

        public void RemoveWithoutLeft()
        {
            SceneController.Instance.RemoveSceneWithout(SceneAddType.LEFT);
        }
        
        public void RemoveWithoutRight()
        {
            SceneController.Instance.RemoveSceneWithout(SceneAddType.RIGHT);
        }
        
        public void ReplaceScene()
        {
            SceneController.Instance.ReplaceScene(inputField.text);
        }

        public void GoToMain()
        {
            SceneController.Instance.ReplaceScene("MainScene");
        }
    }
}