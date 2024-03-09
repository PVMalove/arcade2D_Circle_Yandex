using System;
using System.IO;
using System.Threading;
using CodeBase.Core.Data;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using YG;

namespace CodeBase.Core.Services.SaveLoadService
{
    public class LoadService : ILoadService, IDisposable
    {
        private readonly ILogService log;
        private readonly string filePath;
        private readonly CancellationTokenSource ctn = new CancellationTokenSource();

        public LoadService(ILogService log)
        {
            this.log = log;
#if UNITY_EDITOR
            filePath = $"{Application.persistentDataPath}/Save.json";
#endif
        }

        public async UniTask<PlayerProgress> LoadProgress()
        {
#if !UNITY_EDITOR
            if (YandexGame.SDKEnabled)
                return await LoadProgressYandexAsync();
            else
                return null;
#elif UNITY_EDITOR
            return await LoadProgressDefault();
#endif
        }

        private async UniTask<PlayerProgress> LoadProgressDefault()
        {
            string json = "";
            if (!File.Exists(filePath))
            {
                return null;
            }

            using StreamReader reader = new(filePath);
            while (await reader.ReadLineAsync().AsUniTask() is { } line)
                json += line;

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            
            PlayerProgress userData = json.ToDeserialized<PlayerProgress>();
            return userData;
        }

        [UsedImplicitly]
        private async UniTask<PlayerProgress> LoadProgressYandexAsync()
        {
            try
            {
                string json = await YandexGame.LoadProgressPlayerDataAsync(ctn.Token);
                log.LogYandex($"LoadProgressYandexAsync -> json {json}", this);
                
                if (json == String.Empty)
                {
                    log.LogYandex($"Player data null: {json}", this);
                    return null;
                }

                PlayerProgress userData = json.ToDeserialized<PlayerProgress>();
                log.LogYandex($"Player data -> AudioControlData : {JsonUtility.ToJson(userData.AudioControlData)}", this);
                return userData;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            ctn?.Cancel();
        }
    }
}