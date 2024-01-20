using CodeBase.Core.GameFlow.GameMode.GameWorld;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class StartGameModeState : IState
    {
        private readonly IInitializeGameWorld gameWorld;
        private readonly SceneStateMachine sceneStateMachine;
        private readonly ILogService log;

        public StartGameModeState(IInitializeGameWorld gameWorld,
            SceneStateMachine sceneStateMachine,
            ILogService log)
        {
            this.gameWorld = gameWorld;
            this.sceneStateMachine = sceneStateMachine;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            gameWorld.InitGameWorld();
            await sceneStateMachine.Enter<PlayGameModeState>();
        }

        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return default;
        }
    }
}