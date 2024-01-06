using System;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Services.Factories;
using UnityEngine;

namespace CodeBase.Core.GameFlow.GameMode.GameWorld
{
    public class InitializeGameWorld : IInitializeGameWorld, IDisposable
    {
        private readonly IGameFactory gameFactory;
        private readonly IUIFactory uiFactory;
        private readonly AutoSaveData.Factory autoSaveDataFactory;
        private readonly IPersistentProgressStorage progressStorage;
        private readonly IHUDService hudService;
        private readonly ILogService log;

        private BuildInfoConfig buildInfoConfig;
        
        public InitializeGameWorld(IGameFactory gameFactory, IUIFactory uiFactory,
            AutoSaveData.Factory autoSaveDataFactory,
            IPersistentProgressStorage progressStorage,
            IHUDService hudService,
            ILogService log)
        {
            this.gameFactory = gameFactory;
            this.uiFactory = uiFactory;
            this.autoSaveDataFactory = autoSaveDataFactory;
            this.progressStorage = progressStorage;
            this.hudService = hudService;
            this.log = log;
        }
        
        public void InitGameWorld()
        {
            buildInfoConfig = new BuildInfoConfig();
            buildInfoConfig.BuildNumber = Application.version;

            //Infrastructure
            autoSaveDataFactory.Create();
            uiFactory.CreateUIRoot();

            //UI      
            gameFactory.CreateHUD();
            hudService.ShowBuildInfo(buildInfoConfig);
            hudService.ShowSettingBar();
            
            //Gameplay

            
            LoadProgressReader();
        }
        
        private void LoadProgressReader()
        {
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressStorage.Progress);
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressStorage.Progress);
            log.LogState("Notify progress reader complete load data for object", this);
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}