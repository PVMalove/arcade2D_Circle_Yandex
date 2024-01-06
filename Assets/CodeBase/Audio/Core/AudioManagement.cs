using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace CodeBase.Audio.Core
{
    public class AudioManagement : IAudioManagement
    {
        private Dictionary<string, AudioClip> cechAudio = new Dictionary<string, AudioClip>();
        private readonly ILogService log;
        private readonly IAssetProvider assetProvider;

        public Dictionary<string, AudioClip> CechAudio => cechAudio;
        
        [Inject]
        public AudioManagement(IAssetProvider assetProvider, ILogService log)
        {
            this.assetProvider = assetProvider;
            this.log = log;
        }

        public async UniTask Initialize()
        {
             await PreloadAudio();

             log.LogAudio("Initialize complete",this);
        }

        public async UniTask<AudioClip> GetClip(string key)
        {
            log.LogAudio("GetClip",this);

            if (cechAudio.TryGetValue(key, out AudioClip clip)) 
                return clip;
            
            log.LogAudio("Get clip from Addressable",this);
            return await LoadClip(key);
        }

        private async UniTask PreloadAudio()
        {
            AudioClip[] audioClips = await GetAudio<AudioClip>();
            cechAudio = audioClips.ToDictionary(audioClip => audioClip.name, audioClip => audioClip);
        }
        private async UniTask<List<string>> GetAudioKeys<TAudio>() => 
            await assetProvider.GetAssetsListByLabel<TAudio>(AssetLabels.Audio);
        
        private async UniTask<TAudio[]> GetAudio<TAudio>() where TAudio : class
        {
            List<string> keys = await GetAudioKeys<TAudio>();
            return await assetProvider.LoadAll<TAudio>(keys);
        }
        
        private async UniTask<AudioClip> LoadClip(string key)
        {
            float startTime = Time.time;
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(key);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                cechAudio[key] = handle.Result;
                log.LogAudio("Audio clip loaded: " + key + " time: " + (Time.time - startTime),this);
                return handle.Result;
            }
            log.LogAudio($"Audio clip name: null", this);
            return null;
        }
    }
}
