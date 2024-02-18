using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.AwaitingOverlay;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameModeState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IAwaitingOverlay awaitingOverlay;
        private readonly ISceneLoader sceneLoader;
        private readonly ILogService log;
        private readonly IAssetProvider assetProvider;

        public GameModeState(ILoadingCurtain loadingCurtain, IAwaitingOverlay awaitingOverlay, 
            ISceneLoader sceneLoader, IAssetProvider assetProvider, ILogService log)
        {
            this.loadingCurtain = loadingCurtain;
            this.awaitingOverlay = awaitingOverlay;
            this.sceneLoader = sceneLoader;
            this.assetProvider = assetProvider;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            loadingCurtain.Show();
            awaitingOverlay.Show();
            await assetProvider.WarmupAssetsByLabel(AssetLabels.GameplayState);
            await sceneLoader.Load(InfrastructureAssetPath.GameScene);
        }

        public async UniTask Exit()
        {
            await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameplayState);
            log.LogState("Exit", this);
        }
    }
}