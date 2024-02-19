using System;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Gameplay.Environment;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Popups.Service;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Windows.Service;
using UnityEngine;


namespace CodeBase.Core.GameFlow.GameMode.GameWorld
{
    public class InitializeGameWorld : IInitializeGameWorld, IDisposable
    {
        private readonly IGameFactory gameFactory;
        private readonly IUIFactory uiFactory;
        private readonly AutoSaveData.Factory autoSaveDataFactory;
        private readonly IPersistentProgressStorage progressStorage;
        private readonly IWindowService windowService;
        private readonly IHUDService hudService;
        private readonly IPopupService popupService;
        private readonly ILogService log;
        

        private BuildInfoConfig buildInfoConfig;

        public InitializeGameWorld(IGameFactory gameFactory, IUIFactory uiFactory,
            AutoSaveData.Factory autoSaveDataFactory,
            IPersistentProgressStorage progressStorage,
            IWindowService windowService,
            IHUDService hudService,
            IPopupService popupService,
            ILogService log)
        {
            this.gameFactory = gameFactory;
            this.uiFactory = uiFactory;
            this.autoSaveDataFactory = autoSaveDataFactory;
            this.progressStorage = progressStorage;
            this.windowService = windowService;
            this.hudService = hudService;
            this.popupService = popupService;
            this.log = log;
        }

        public void InitGameWorld()
        {
            buildInfoConfig = new BuildInfoConfig();
            buildInfoConfig.BuildNumber = Application.version;

            //Infrastructure
            autoSaveDataFactory.Create();
            uiFactory.CreateUIRoot();

            //Gameplay
            gameFactory.CreateCircleBackground();
            gameFactory.CircleBackground.GetComponent<CircleBackgroundAnimation>().EndGameAnimation();
            //UI
            //gameFactory.CreateHUD();
            hudService.ShowSettingBar();
            hudService.ShowBuildInfo(buildInfoConfig);
            //windowService.ShowGameMenu();

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