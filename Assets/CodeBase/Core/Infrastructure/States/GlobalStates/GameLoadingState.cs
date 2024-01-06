using CodeBase.Audio.Core;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameLoadingState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly ISceneLoader sceneLoader;
        private readonly IAssetProvider assetProvider;
        private readonly IAudioManagement audioManagement;
        private readonly ILogService log;

        public GameLoadingState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, 
            IAssetProvider assetProvider, IAudioManagement audioManagement, ILogService log)
        {
            this.loadingCurtain = loadingCurtain;
            this.sceneLoader = sceneLoader;
            this.assetProvider = assetProvider;
            this.audioManagement = audioManagement;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            loadingCurtain.Show();
            
            await assetProvider.WarmupAssetsByLabel(AssetLabels.GameLoadingState);
            await audioManagement.Initialize();
            await sceneLoader.Load(InfrastructureAssetPath.GameLoadingScene);
            
            
            loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            loadingCurtain.Show();
            //todo так выгружаются ассеты
            await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameLoadingState);
            log.LogState("Exit", this);
        }
    }
}