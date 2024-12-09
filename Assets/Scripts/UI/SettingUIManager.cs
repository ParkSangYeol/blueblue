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

        [Space(3)]
        [Header("BGM")] [SerializeField] private Slider bgmSlider;
        [Header("SFX")] [SerializeField] private Slider sfxSlider;

        [Space(3)]
        [Header("Sensitivity")]
        [SerializeField] private Slider mouseHorizontalSlider;
        [HideInInspector] public UnityEvent<float> onHorizontalSensitivityValueChanged;
        [SerializeField] private float curHorizontalSensitivity;
        [SerializeField] private float minHorizontalSensitivity = 20f;
        [SerializeField] private float maxHorizontalSensitivity = 200f;

        [SerializeField] private Slider mouseVerticalSlider;
        [HideInInspector]public UnityEvent<float> onVerticalSensitivityValueChanged;
        [SerializeField] private float curVerticalSensitivity;
        [SerializeField] private float minVerticalSensitivity = 20f;
        [SerializeField] private float maxVerticalSensitivity = 200f;

        [Space(3)]
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
            mouseHorizontalSlider.maxValue = maxHorizontalSensitivity;
            mouseHorizontalSlider.minValue = minHorizontalSensitivity;
            curHorizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity", 110);
            mouseHorizontalSlider.value = curHorizontalSensitivity;
            mouseHorizontalSlider.onValueChanged.AddListener(OnHorizontalSensitivityValueChanged);

            mouseVerticalSlider.maxValue = maxVerticalSensitivity;
            mouseVerticalSlider.minValue = minVerticalSensitivity;
            curVerticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity", 110);
            mouseVerticalSlider.value = curVerticalSensitivity;
            mouseVerticalSlider.onValueChanged.AddListener(OnVerticalSensitivityValueChanged);
            
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

        private void OnVerticalSensitivityValueChanged(float value)
        {
            curVerticalSensitivity = value;
            PlayerPrefs.SetFloat("VerticalSensitivity", curVerticalSensitivity);
            onVerticalSensitivityValueChanged.Invoke(value);
        }
        
        private void OnHorizontalSensitivityValueChanged(float value)
        {
            curHorizontalSensitivity = value;
            PlayerPrefs.SetFloat("HorizontalSensitivity", curHorizontalSensitivity);
            onHorizontalSensitivityValueChanged.Invoke(value);
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
