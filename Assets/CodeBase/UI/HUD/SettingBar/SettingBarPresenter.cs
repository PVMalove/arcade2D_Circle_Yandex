using System;
using System.Collections.Generic;
using CodeBase.Audio.Service;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.ProgressService;
using CodeBase.StaticData.Infrastructure;
using CodeBase.UI.HUD.Service;
using UnityEngine;
using YG;
using Zenject;

namespace CodeBase.UI.HUD.SettingBar
{
    public sealed class SettingBarPresenter : ISettingBarPresenter
    {
        public event Action<bool> OnChangedMusicState;
        public event Action<bool> OnChangedFXState;
        
        private readonly IPersistentProgressService progressService;
        private readonly IAssetProvider assetProvider;
        private readonly IAudioService audioService;
        private readonly IGameFactory gameFactory;
        private readonly IHUDService hudService;

        public float AudioVolume { get; private set; }
        public bool MusicOn { get; private set; }
        public bool EffectsOn { get; private set; }

        public SettingBarPresenter(IPersistentProgressService progressService, IAssetProvider assetProvider,
            IAudioService audioService, IGameFactory gameFactory, IHUDService hudService)
        {
            this.progressService = progressService;
            this.assetProvider = assetProvider;
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
            AudioVolume = progressService.GetProgress().AudioControlData.AudioVolume;
            MusicOn = progressService.GetProgress().AudioControlData.MusicOn;
            EffectsOn = progressService.GetProgress().AudioControlData.EffectsOn;
            UpdateSettingState();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progressService.GetProgress().AudioControlData.AudioVolume = AudioVolume;
            progressService.GetProgress().AudioControlData.MusicOn = MusicOn;
            progressService.GetProgress().AudioControlData.EffectsOn = EffectsOn;
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

        private async void NewProgress()
        {
            Debug.Log("Reset player progress");
            YandexGame.ResetSaveProgress();
            FirstSaveData newSaveData = await assetProvider.Load<FirstSaveData>(InfrastructureAssetPath.NewSaveDataAddress);
            
            PlayerOwnedItems ownedItems = new PlayerOwnedItems(
                new List<string> { newSaveData.circleHeroGUID });
            
            AudioControlData audioControl = new AudioControlData(
                newSaveData.AudioVolume, 
                newSaveData.MusicOn,
                newSaveData.EffectsOn
            );
            
            PlayerProgress progress = new PlayerProgress(
                audioControl,
                newSaveData.circleHeroGUID,
                ownedItems);

            progressService.Initialize(progress);
            
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            
            Debug.Log("Init new player progress");
        }

        public sealed class Factory : PlaceholderFactory<ISettingBarPresenter> { }
    }
}