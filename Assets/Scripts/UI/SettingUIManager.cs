using com.kleberswf.lib.core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingUIManager : Singleton<SettingUIManager>
    {
        [Header("UI Canvas")] 
        [Tooltip("���� UI ĵ����")][SerializeField]private GameObject settingUICanvas;
        public GameObject SettingUICanvas => settingUICanvas;
        [Tooltip("���� Ȱ��ȭ ��ư")] public KeyCode settingKeyCode = KeyCode.Escape;

        [Header("BGM")] [SerializeField] private Slider bgmSlider;
        [Header("SFX")] [SerializeField] private Slider sfxSlider;
        [Header("Sensitivity")] [SerializeField] private Slider mouseSlider;
        [Header("Button")] 
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button exitBtn;

        // Start is called before the first frame update
        void Start()
        {
            Invoke(nameof(Initialize), 0.1f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(settingKeyCode))
            {
                settingUICanvas.SetActive(!settingUICanvas.activeSelf);
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

            #endregion
            
            #region Button

            continueBtn.onClick.AddListener(OnClickContinueBtn);
            exitBtn.onClick.AddListener(OnClickExitBtn);

            #endregion
            
        }

        private void OnClickContinueBtn()
        {
            settingUICanvas.SetActive(!settingUICanvas.activeSelf);
        }

        private void OnClickExitBtn()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
        }

        private void OnBGMValueChanged(float volume)
        {
            SoundManager.Instance.ChangeBGMVolume(volume);
            Debug.Log("BGM Volume : " + volume);
        }
        private void OnSFXValueChanged(float volume)
        {
            SoundManager.Instance.ChangeSFXVolume(volume);
            Debug.Log("SFX Volume : " + volume);
        }
    }
}
