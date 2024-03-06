using System;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Gameplay.Environment;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Windows.Service;
using UnityEngine;


namespace CodeBase.Core.GameFlow.GameMode.GameWorld
{
    public class InitializeGameWorld : IInitializeGameWorld, IDisposable
    {
        private readonly IGameFactory gameFactory;
        private readonly IUIFactory uiFactory;
        private readonly IPersistentProgressService progressService;
        private readonly IWindowService windowService;
        private readonly IHUDService hudService;
        private readonly ILogService log;
        private readonly AutoSaveData.Factory autoSaveDataFactory;

        private BuildInfoConfig buildInfoConfig;

        public InitializeGameWorld(IGameFactory gameFactory, IUIFactory uiFactory,
            IPersistentProgressService progressService,
            IWindowService windowService,
            IHUDService hudService,
            ILogService log,
            AutoSaveData.Factory autoSaveDataFactory)
        {
            this.gameFactory = gameFactory;
            this.uiFactory = uiFactory;
            this.progressService = progressService;
            this.windowService = windowService;
            this.hudService = hudService;
            this.log = log;
            this.autoSaveDataFactory = autoSaveDataFactory;
        }

        public async void InitGameWorld()
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
            gameFactory.CreateHUD();
            hudService.ShowSettingBar();
            hudService.ShowBuildInfo(buildInfoConfig);
            windowService.ShowGameMenu();

            await gameFactory.CreateCircleHero();

            LoadProgressReader();
        }

        private void LoadProgressReader()
        {
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            log.LogState("Notify progress reader complete load data for object", this);
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}