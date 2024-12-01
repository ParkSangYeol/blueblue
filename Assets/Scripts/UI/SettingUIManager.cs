using com.kleberswf.lib.core;
using PlayerControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class SettingUIManager : Singleton<SettingUIManager>
    {
        [Header("UI Canvas")] 
        [Tooltip("설정 UI 캔버스")][SerializeField]private GameObject settingUICanvas;
        public GameObject SettingUICanvas => settingUICanvas;
        [Tooltip("설정 키")] public KeyCode settingKeyCode = KeyCode.Escape;

        [Header("BGM")] [SerializeField] private Slider bgmSlider;
        [Header("SFX")] [SerializeField] private Slider sfxSlider;
        [Header("Sensitivity")] 
        public UnityEvent onSensitivityValueChanged;
        [SerializeField] private Slider mouseSlider;
        [SerializeField] private float curSensitivity;
        [SerializeField]private float minSensitivity = 50f;
        [SerializeField]private float maxSensitivity = 150f;
        [Header("Button")] 
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button exitBtn;

        // Start is called before the first frame update
        void Start()
        {
            Invoke(nameof(Initialize), 0.1f);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(settingKeyCode))
            {
                SwitchCanvas();
            }
        }

        private void Initialize()
        {
            settingUICanvas.SetActive(false);

            #region BGM

            var bgmVolume = SoundManager.Instance.BGMMasterVolume;
            bgmSlider.value = bgmVolume;
            bgmSlider.onValueChanged.AddListener(OnBGMValueChanged);
            #endregion  
            
            #region SFX

            var sfxVolume = SoundManager.Instance.SFXMasterVolume;
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(OnSFXValueChanged);
            #endregion   
            
            #region Mouse

            curSensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
            mouseSlider.value = curSensitivity;
            mouseSlider.maxValue = maxSensitivity;
            mouseSlider.minValue = minSensitivity;
            mouseSlider.onValueChanged.AddListener(OnSensitivityValueChanged);
            
            #endregion
            
            #region Button

            continueBtn.onClick.AddListener(OnClickContinueBtn);
            exitBtn.onClick.AddListener(OnClickExitBtn);

            #endregion
            
        }

        #region Slider
        
        private void OnBGMValueChanged(float volume)
        {
            SoundManager.Instance.ChangeBGMVolume(volume);
        }
        private void OnSFXValueChanged(float volume)
        {
            SoundManager.Instance.ChangeSFXVolume(volume);
        }

        private void OnSensitivityValueChanged(float value)
        {
            curSensitivity = value;
            PlayerPrefs.SetFloat("Sensitivity", curSensitivity);
            onSensitivityValueChanged.Invoke();
        }

        public float GetSensitivity()
        {
            return curSensitivity;
        }
        
        #endregion
        
        #region Button

        private void OnClickContinueBtn()
        {
            SwitchCanvas();
        }

        private void OnClickExitBtn()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        #endregion


        private void SwitchCanvas()
        {
            settingUICanvas.SetActive(!settingUICanvas.activeSelf);
            Cursor.visible = settingUICanvas.activeSelf;
            if (settingUICanvas.activeSelf) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
