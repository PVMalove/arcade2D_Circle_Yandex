using CodeBase.Core.GameFlow.GameMode.States;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Core.GameFlow.GameMode
{
    public class GameModeSceneBootstraper : IInitializable
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly StatesFactory statesFactory;
        private readonly ILogService log;

        public GameModeSceneBootstraper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory,
            ILogService log)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.statesFactory = statesFactory;
            this.log = log;
        }

        public void Initialize()
        {
            log.LogState("Start game mode scene bootstraping", this);

            sceneStateMachine.RegisterState(statesFactory.Create<StartGameModeState>());
            sceneStateMachine.RegisterState(statesFactory.Create<PlayGameModeState>());
            sceneStateMachine.RegisterState(statesFactory.Create<WinGameModeState>());
            sceneStateMachine.RegisterState(statesFactory.Create<FailGameModeState>());
            sceneStateMachine.RegisterState(statesFactory.Create<FinishGameModeState>());

            log.LogState("Finish game mode scene bootstraping", this);

            // go to the first scene state
            sceneStateMachine.Enter<StartGameModeState>().Forget();
        }
    }
}