using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameModeState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly ISceneLoader sceneLoader;
        private readonly ILogService log;
        private readonly IAssetProvider assetProvider;

        public GameModeState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, ILogService log, IAssetProvider assetProvider)
        {
            this.loadingCurtain = loadingCurtain;
            this.sceneLoader = sceneLoader;
            this.log = log;
            this.assetProvider = assetProvider;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            loadingCurtain.Show();
            await assetProvider.WarmupAssetsByLabel(AssetLabels.GameplayState);
            await sceneLoader.Load(InfrastructureAssetPath.GameModeScene);
            loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            loadingCurtain.Show();
            await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameplayState);
            log.LogState("Exit", this);
        }
    }
}