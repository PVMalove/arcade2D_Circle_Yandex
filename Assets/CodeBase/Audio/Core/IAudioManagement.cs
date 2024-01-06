using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Audio.Core
{
    public interface IAudioManagement
    {
        UniTask<AudioClip> GetClip(string key);
        Dictionary<string, AudioClip> CechAudio { get; }
        UniTask Initialize();
    }
}