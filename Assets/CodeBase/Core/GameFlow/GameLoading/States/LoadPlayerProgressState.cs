using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.StaticData;
using Cysharp.Threading.Tasks;
using YG;

namespace CodeBase.Core.GameFlow.GameLoading.States
{
    public class LoadPlayerProgressState : IState
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly IAssetProvider assetProvider;
        private readonly IPersistentProgressStorage progressStorage;
        private readonly ILoadService loadService;
        private readonly ILogService log;

        public LoadPlayerProgressState(SceneStateMachine sceneStateMachine,
            IAssetProvider assetProvider,
            IPersistentProgressStorage progressStorage, 
            ILoadService loadService,
            ILogService log)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.assetProvider = assetProvider;
            this.progressStorage = progressStorage;
            this.loadService = loadService;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            YandexGame.GameReadyAPI();
            await loadService.Subscribe(OnCompleteLoadData);
            loadService.LoadProgress();
            await sceneStateMachine.Enter<FinishGameLoadingState>();
        }
        
        private async void OnCompleteLoadData(PlayerProgress dataProgress)
        {
            log.LogState("OnCompleteLoadData player progress", this);
            progressStorage.Progress = dataProgress ?? await NewProgress();
            loadService.Unsubscribe(OnCompleteLoadData);
        }

        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return default;
        }

        private async UniTask<PlayerProgress> NewProgress()
        {
            log.LogState("Start init new player progress", this);
            
            FirstSaveData newSaveData = await assetProvider.Load<FirstSaveData>(InfrastructureAssetPath.NewSaveDataAddress);
            
            AudioControlData audioControl = new AudioControlData(
                newSaveData.AudioVolume, 
                newSaveData.MusicOn,
                newSaveData.EffectsOn
            );
            
            PlayerOwnedItems ownedItems = new PlayerOwnedItems(
                new List<string> { newSaveData.circleHeroGUID });

            PlayerProgress progress = new PlayerProgress(ownedItems, audioControl);
            log.LogState("Init new player progress", this);
            return progress;
        }
    }
}