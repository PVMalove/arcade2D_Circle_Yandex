using System.IO;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.ProgressService;
using CodeBase.UI.HUD.Service;
using UnityEngine;
using YG;

namespace CodeBase.Core.Services.SaveLoadService
{
    public class SaveService : ISaveService
    {
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;
        private readonly IHUDService hudService;
        private readonly string filePath;

        public SaveService(IGameFactory gameFactory, IPersistentProgressService progressService,
            IHUDService hudService)
        {
            this.gameFactory = gameFactory;
            this.progressService = progressService;
            this.hudService = hudService;
            filePath = $"{Application.persistentDataPath}/Save.json";
        }

        public void SaveProgress()
        {
            foreach (IProgressSaver progressWriter in gameFactory.ProgressWriters) 
                progressWriter.UpdateProgress(progressService.GetProgress());
            foreach (IProgressSaver progressWriter in hudService.ProgressWriters) 
                progressWriter.UpdateProgress(progressService.GetProgress());

            string json = progressService.GetProgress().ToJson();

#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGame.SaveProgressPlayerData(json);
#elif UNITY_EDITOR
            using StreamWriter writer = new(filePath);
            writer.Write(json);
            writer.Close();
#endif
        }
    }
}