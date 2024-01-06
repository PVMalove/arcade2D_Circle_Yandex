using System;
using CodeBase.Audio.Core;

namespace CodeBase.Audio.Service
{
    public interface IAudioService
    {
        event Action<bool> OnChangedMuteMusicState;
        
        SourceAudio MusicSourceAudio { get; }
        SourceAudio FXSourceAudio { get; }

        void ChangeVolume(float value);
        void ToggleMusic(bool isOn);
        void ToggleEffects(bool isOn);
        event Action<bool> OnChangedMuteFXState;
    }
}