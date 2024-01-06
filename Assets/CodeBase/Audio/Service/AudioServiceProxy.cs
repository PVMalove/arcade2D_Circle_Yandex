using System;
using CodeBase.Audio.Core;

namespace CodeBase.Audio.Service
{
    public class AudioServiceProxy : IAudioService
    {
        public event Action<bool> OnChangedMuteMusicState;
        public event Action<bool> OnChangedMuteFXState;

        private readonly AudioService.Factory factory;
        private IAudioService impl;

        public AudioServiceProxy(AudioService.Factory factory) =>
            this.factory = factory;

        public void Initialize()
        {
            impl = factory.Create();
            impl.OnChangedMuteMusicState += HandleMuteStateMusicChanged;
            impl.OnChangedMuteFXState += HandleMuteStateFXChanged;
        }

        public SourceAudio MusicSourceAudio => impl.MusicSourceAudio;
        public SourceAudio FXSourceAudio => impl.FXSourceAudio;

        public void ChangeVolume(float value) => impl.ChangeVolume(value);

        public void ToggleMusic(bool isOn) => impl.ToggleMusic(isOn);

        public void ToggleEffects(bool isOn) => impl.ToggleEffects(isOn);

        private void HandleMuteStateMusicChanged(bool value) =>
            OnChangedMuteMusicState?.Invoke(value);
        private void HandleMuteStateFXChanged(bool value) =>
            OnChangedMuteFXState?.Invoke(value);
    }
}