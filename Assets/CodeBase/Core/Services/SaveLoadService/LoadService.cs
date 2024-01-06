using System;
using System.IO;
using CodeBase.Core.Data;
using CodeBase.Core.Services.LogService;
using UnityEngine;
using YG;

namespace CodeBase.Core.Services.SaveLoadService
{
    public class LoadService : ILoadService
    {
        private readonly ILogService log;
        private Action<PlayerProgress> listeners;
        private readonly string filePath;

        public LoadService(ILogService log)
        {
            this.log = log;
            filePath = $"{Application.persistentDataPath}/Save.json";
            YandexGame.YandexDataCloudListeners += UpdateDataFromCloud;
        }
        
        public void Subscribe(Action<PlayerProgress> onComplete) => listeners += onComplete;
        public void Unsubscribe(Action<PlayerProgress> onComplete) => listeners -= onComplete;
        
        public void LoadProgress()
        {
#if !UNITY_EDITOR
            if (YandexGame.SDKEnabled)
                LoadProgressYandex();
#elif UNITY_EDITOR
            LoadProgressDefault();
#endif
        }
        
        private void LoadProgressDefault()
        {
            string json = "";
            if (!File.Exists(filePath))
            {
                listeners?.Invoke(null);
                return;
            }

            using StreamReader reader = new(filePath);
            while (reader.ReadLine() is { } line)
                json += line;

            if (string.IsNullOrEmpty(json))
            {
                listeners?.Invoke(null);
                return;
            }

            PlayerProgress userData = JsonUtility.FromJson<PlayerProgress>(json);
            listeners?.Invoke(userData);
        }
        
        private void LoadProgressYandex()
        {
            YandexGame.LoadProgressPlayerData();
        }
        
        private void UpdateDataFromCloud(string json)
        {
            PlayerProgress userData;
            if (json == String.Empty)
            {
                userData = null;
                log.LogYandex($"Player data null: {json}", this); 
            }
            else
            {
                userData = JsonUtility.FromJson<PlayerProgress>(json);
                log.LogYandex($"Player data: {JsonUtility.ToJson(userData.AudioControlData)}", this);
            }

            listeners?.Invoke(userData);
        }
    }
}