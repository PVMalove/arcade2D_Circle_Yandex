using CodeBase.Core.GameFlow.GameMode.GameWorld;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.AwaitingOverlay;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Gameplay.Environment;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class StartGameModeState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IAwaitingOverlay awaitingOverlay;
        private readonly IInitializeGameWorld gameWorld;
        private readonly SceneStateMachine sceneStateMachine;
        private readonly ILogService log;


        public StartGameModeState(ILoadingCurtain loadingCurtain,
            IAwaitingOverlay awaitingOverlay,
            IInitializeGameWorld gameWorld, 
            SceneStateMachine sceneStateMachine, ILogService log)
        {
            this.gameWorld = gameWorld;
            this.sceneStateMachine = sceneStateMachine;
            this.log = log;
            this.loadingCurtain = loadingCurtain;
            this.awaitingOverlay = awaitingOverlay;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            gameWorld.InitGameWorld();
            loadingCurtain.Hide();
            awaitingOverlay.Hide();
            await sceneStateMachine.Enter<PlayGameModeState>();
        }

        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return default;
        }
    }
}