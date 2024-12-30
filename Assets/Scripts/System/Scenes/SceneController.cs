using System.Collections.Generic;
using Anomaly;
using com.kleberswf.lib.core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace System.Scenes
{
    public class SceneController : Singleton<SceneController>
    {
        private void Start()
        {
            var loader = GameObject.FindObjectOfType<AnomalyLoader>();
            loader.SetAnomaly(0);
        }

        [Button]
        public void ResetScene(int idx)
        { 
            UnityAction<Scene, LoadSceneMode> LoadMap = null;
            LoadMap = (scene, mode) =>
            {
                var loader = GameObject.FindObjectOfType<AnomalyLoader>();
                loader.SetAnomaly(idx);
                SceneManager.sceneLoaded -= LoadMap;
                
            };
            SceneManager.sceneLoaded += LoadMap;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}