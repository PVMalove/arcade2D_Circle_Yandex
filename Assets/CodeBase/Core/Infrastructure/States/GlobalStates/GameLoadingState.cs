using CodeBase.Audio.Core;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.AwaitingOverlay;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.States.GlobalStates
{
    public class GameLoadingState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IAwaitingOverlay awaitingOverlay;
        private readonly ISceneLoader sceneLoader;
        private readonly IAssetProvider assetProvider;
        private readonly IAudioManagement audioManagement;
        private readonly ILogService log;

        public GameLoadingState(ILoadingCurtain loadingCurtain, IAwaitingOverlay awaitingOverlay, 
            ISceneLoader sceneLoader, IAssetProvider assetProvider, IAudioManagement audioManagement, 
            ILogService log)
        {
            this.loadingCurtain = loadingCurtain;
            this.awaitingOverlay = awaitingOverlay;
            this.sceneLoader = sceneLoader;
            this.assetProvider = assetProvider;
            this.audioManagement = audioManagement;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            loadingCurtain.Show();
            awaitingOverlay.Show();
            await assetProvider.WarmupAssetsByLabel(AssetLabels.GameLoadingState);
            await audioManagement.Initialize();
            await sceneLoader.Load(InfrastructureAssetPath.GameLoadingSceneAddress);
        }

        public async UniTask Exit()
        {
            await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameLoadingState);
            log.LogState("Exit", this);
        }
    }
}