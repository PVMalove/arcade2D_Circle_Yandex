using System;
using CodeBase.Core.Services.PlayerProgressService;

namespace CodeBase.UI.HUD.SettingBar
{
    public interface ISettingBarPresenter : IProgressSaver
    {
        event Action<bool> OnChangedMusicState;
        event Action<bool> OnChangedFXState;
        
        float AudioVolume { get; }
        bool MusicOn { get; }
        bool EffectsOn { get; }
        void ToggleMusic();
        void ToggleEffects();
        void Enable();
        void Disable();
        void ResetProgress();
    }
}