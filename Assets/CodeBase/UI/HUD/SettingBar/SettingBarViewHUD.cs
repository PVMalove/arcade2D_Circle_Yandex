using CodeBase.UI.HUD.Base;
using CodeBase.UI.HUD.SettingBar.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.HUD.SettingBar
{
    public sealed class SettingBarViewHUD : HUDBase<ISettingBarPresenter>
    {
        [Header("Setting panel")] 
        [SerializeField] private Button settingButton;
        
        [SerializeField] private GameObject settingsObject;
        [SerializeField] private SoundButton musicSoundButton;
        [SerializeField] private SoundButton sfxSoundButton;
        [SerializeField] private Button resetProgressButton;
        
        private ISettingBarPresenter setting;
        private VerticalLayoutGroup layoutGroup;
        
        protected override void Initialize(ISettingBarPresenter presenter)
        {
            base.Initialize(presenter);
            this.setting = presenter;
            presenter.Enable();
            presenter.OnChangedMusicState += MusicButtonUpdateState;
            presenter.OnChangedFXState += FXButtonUpdateState;
            layoutGroup = settingsObject.GetComponent<VerticalLayoutGroup>();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            settingButton.onClick.AddListener(OpenSettingOnClick);
            musicSoundButton.AddListener(SoundOnClick);
            sfxSoundButton.AddListener(SFXOnClick);
            resetProgressButton.onClick.AddListener(ResetProgressOnClick);
        }

        private void MusicButtonUpdateState(bool state) => 
            musicSoundButton.ChangeStateSoundButton(state);
        
        private void FXButtonUpdateState(bool state) => 
            sfxSoundButton.ChangeStateSoundButton(state);

        private void OpenSettingOnClick()
        {
            float startValue = -40f;
            float endValue = 0f;
            float duration = 0.5f;
            
            settingsObject.SetActive(!settingsObject.activeSelf);

            if (settingsObject.activeSelf == false)
            {
                layoutGroup.spacing = endValue;
            }
        }

        private void SoundOnClick()
        {
            setting.ToggleMusic();
        }

        private void SFXOnClick()
        {
            setting.ToggleEffects();
        }

        private void ResetProgressOnClick()
        {
            setting.ResetProgress();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            setting.Disable();
            musicSoundButton.RemoveListener(SoundOnClick);
            sfxSoundButton.RemoveListener(SFXOnClick);
            setting.OnChangedMusicState -= MusicButtonUpdateState;
            setting.OnChangedFXState -= FXButtonUpdateState;
        }


        public class Factory : PlaceholderFactory<SettingBarViewHUD> { }
    }
}