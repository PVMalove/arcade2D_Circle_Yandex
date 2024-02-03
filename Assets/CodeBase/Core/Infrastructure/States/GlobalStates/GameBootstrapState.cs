using CodeBase.Audio.Service;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.StaticDataService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IStaticDataService staticDataService;
        private readonly ILogService log;
        private readonly LoadingCurtainProxy loadingCurtainProxy;
        private readonly AudioServiceProxy audioServiceProxy;
        private readonly IAssetProvider assetProvider;

        public GameBootstrapState(GameStateMachine gameStateMachine,
            IStaticDataService staticDataService,
            IAssetProvider assetProvider,
            LoadingCurtainProxy loadingCurtainProxy,
            AudioServiceProxy audioServiceProxy,
            ILogService log)
        {
            this.gameStateMachine = gameStateMachine;
            this.staticDataService = staticDataService;
            this.assetProvider = assetProvider;
            this.loadingCurtainProxy = loadingCurtainProxy;
            this.audioServiceProxy = audioServiceProxy;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            await InitServices();
            gameStateMachine.Enter<GameLoadingState>().Forget();
        }

        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return default;
        }

        private async UniTask InitServices()
        {
            // init global services that may need initialization in some order here
            await assetProvider.InitializeAsync();
            await staticDataService.InitializeAsync();
            await loadingCurtainProxy.InitializeAsync();
            audioServiceProxy.Initialize();
        }
    }
}