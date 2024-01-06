using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.AwaitingOverlay;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using YG;

namespace CodeBase.Core.GameFlow.GameLoading.States
{
    public class LoadPlayerProgressState : IState
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly IPersistentProgressStorage progressStorage;
        private readonly ILoadService loadService;
        private readonly IAwaitingOverlay awaitingOverlay;
        private readonly IStaticDataService staticDataService;
        private readonly ILogService log;

        public LoadPlayerProgressState(SceneStateMachine sceneStateMachine, IPersistentProgressStorage progressStorage, 
            ILoadService loadService,
            IAwaitingOverlay awaitingOverlay,IStaticDataService staticDataService, ILogService log)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.progressStorage = progressStorage;
            this.loadService = loadService;
            this.awaitingOverlay = awaitingOverlay;
            this.staticDataService = staticDataService;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            YandexGame.GameReadyAPI();
            awaitingOverlay.Show("Loading player progress...");

            loadService.Subscribe(OnCompleteLoadData);

            loadService.LoadProgress();
            
            //await UniTask.WaitForSeconds(1f); // just for demonstrate concept with overlay. You can remove it. 

            awaitingOverlay.Hide();

            sceneStateMachine.Enter<FinishGameLoadingState>().Forget();
        }
        
        private void OnCompleteLoadData(PlayerProgress dataProgress)
        {
            progressStorage.Progress = dataProgress ?? NewProgress();
            loadService.Unsubscribe(OnCompleteLoadData);
        }

        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return default;
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress();

            progress.AudioControlData.AudioVolume = 0.5f;
            progress.AudioControlData.EffectsOn = true;
            progress.AudioControlData.MusicOn = true;
            log.LogState("Init new player progress", this);
            
            return progress;
        }
    }
}