using System.Collections.Generic;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.StaticData.Infrastructure;
using Cysharp.Threading.Tasks;
using YG;

namespace CodeBase.Core.GameFlow.GameLoading.States
{
    public class LoadPlayerProgressState : IState
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly IAssetProvider assetProvider;
        private readonly IPersistentProgressService progressService;
        private readonly ILoadService loadService;
        private readonly ILogService log;

        public LoadPlayerProgressState(SceneStateMachine sceneStateMachine,
            IAssetProvider assetProvider,
            IPersistentProgressService progressService, 
            ILoadService loadService,
            ILogService log)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.assetProvider = assetProvider;
            this.progressService = progressService;
            this.loadService = loadService;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            YandexGame.GameReadyAPI();
            await CompleteLoadData();
            await sceneStateMachine.Enter<FinishGameLoadingState>();
        }
        
        private async UniTask CompleteLoadData()
        {
            PlayerProgress progress = await loadService.LoadProgress();
            progressService.Initialize(progress ?? await NewProgress());
            log.LogState($"CompleteLoadData player progress: {progress}", this);
        }

        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return UniTask.CompletedTask;
        }

        private async UniTask<PlayerProgress> NewProgress()
        {
            FirstSaveData newSaveData = await assetProvider.Load<FirstSaveData>(InfrastructureAssetPath.NewSaveDataAddress);
            
            AudioControlData audioControl = new AudioControlData(
                newSaveData.AudioVolume, 
                newSaveData.MusicOn,
                newSaveData.EffectsOn
            );
            
            PlayerOwnedItems ownedItems = new PlayerOwnedItems(
                new List<string> { newSaveData.circleHeroGUID });

            PlayerProgress progress = new PlayerProgress(
                audioControl,
                newSaveData.circleHeroGUID,
                ownedItems);
            
            log.LogState("Init new player progress", this);
            return progress;
        }
    }
}