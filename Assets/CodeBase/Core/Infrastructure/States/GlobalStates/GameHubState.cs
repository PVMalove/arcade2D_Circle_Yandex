using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameHubState : IState
    {
 private readonly ILoadingCurtain loadingCurtain;
        private readonly ISceneLoader sceneLoader;
        private readonly ILogService log;
        private readonly IAssetProvider assetProvider;

        public GameHubState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader,
            ILogService log, IAssetProvider assetProvider)
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

            await assetProvider.WarmupAssetsByLabel(AssetLabels.GameHubState);
            // due to we don't have any substates for this state jet we just load scene with game hub decorations
            await sceneLoader.Load(InfrastructureAssetPath.GameHubScene);
            loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            loadingCurtain.Show();
            await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameHubState);
            log.LogState("Exit", this);
        }
    }
}