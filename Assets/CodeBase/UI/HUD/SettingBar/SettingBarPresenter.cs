using System;
using CodeBase.Audio.Service;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.HUD.Service;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.HUD.SettingBar
{
    public sealed class SettingBarPresenter : ISettingBarPresenter
    {
        public event Action<bool> OnChangedMusicState;
        public event Action<bool> OnChangedFXState;
        
        private readonly IPersistentProgressStorage progressStorage;
        private readonly IAudioService audioService;
        private readonly IGameFactory gameFactory;
        private readonly IHUDService hudService;

        public float AudioVolume { get; private set; }
        public bool MusicOn { get; private set; }
        public bool EffectsOn { get; private set; }

        public SettingBarPresenter(IPersistentProgressStorage progressStorage, IAudioService audioService,
            IGameFactory gameFactory, IHUDService hudService)
        {
            this.progressStorage = progressStorage;
            this.audioService = audioService;
            this.gameFactory = gameFactory;
            this.hudService = hudService;
        }
        
        public void Enable()
        {
            audioService.OnChangedMuteMusicState += OnMuteStateMusicChanged;
            audioService.OnChangedMuteFXState += OnMuteStateFXChanged;
        }

        public void Disable()
        {
            audioService.OnChangedMuteMusicState -= OnMuteStateMusicChanged;
            audioService.OnChangedMuteFXState -= OnMuteStateFXChanged;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            AudioVolume = progressStorage.Progress.AudioControlData.AudioVolume;
            MusicOn = progressStorage.Progress.AudioControlData.MusicOn;
            EffectsOn = progressStorage.Progress.AudioControlData.EffectsOn;
            UpdateSettingState();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progressStorage.Progress.AudioControlData.AudioVolume = AudioVolume;
            progressStorage.Progress.AudioControlData.MusicOn = MusicOn;
            progressStorage.Progress.AudioControlData.EffectsOn = EffectsOn;
        }

        public void ToggleMusic()
        {
            MusicOn = !MusicOn;
            audioService.ToggleMusic(MusicOn);
        }

        public void ToggleEffects()
        {
            EffectsOn = !EffectsOn;
            audioService.ToggleEffects(EffectsOn);
        }
        
        public void ResetProgress()
        {
            NewProgress();
        }

        private void OnMuteStateMusicChanged(bool state) => 
            OnChangedMusicState?.Invoke(state);

        private void OnMuteStateFXChanged(bool state) => 
            OnChangedFXState?.Invoke(state);

        private void UpdateSettingState() 
        {
            audioService.ChangeVolume(AudioVolume);
            audioService.ToggleMusic(MusicOn);
            audioService.ToggleEffects(EffectsOn);
        }

        private void NewProgress()
        {
            Debug.Log("Reset player progress");
            PlayerProgress progress = new();
            progress.AudioControlData.EffectsOn = true;
            progress.AudioControlData.MusicOn = true;
            progress.AudioControlData.AudioVolume = 0.5f;
            progressStorage.Progress = progress;
            
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressStorage.Progress);
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressStorage.Progress);
        }

        public sealed class Factory : PlaceholderFactory<ISettingBarPresenter>
        {
        }
    }
}