using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.GlobalStates;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using CodeBase.UI.Elements;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameHUD.State
{
    public class FinishHUDSceneState : IPaylodedState<TargetStates>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly ILogService log;

        public FinishHUDSceneState(GameStateMachine gameStateMachine, ILogService log)
        {
            this.gameStateMachine = gameStateMachine;
            this.log = log;
        }

        public async UniTask Enter(TargetStates payloadTarget)
        {
            log.LogState("Enter", this);
            gameStateMachine.OnExitState += HandleExitState;
            switch (payloadTarget)
            {
                case TargetStates.Loading:
                    await gameStateMachine.Enter<GameLoadingState>();
                    break;
                case TargetStates.GameHub:
                    await gameStateMachine.Enter<GameHubState>();
                    break;
                case TargetStates.Gameplay:
                    await gameStateMachine.Enter<GameModeState>();
                    break;

                default:
                    log.LogError("Not valid option");
                    break;
            }
        }

        public UniTask Exit()
        {
            gameStateMachine.OnExitState -= HandleExitState;
            log.LogState("Exit", this);
            return default;
        }

        private void HandleExitState()
        {
            if (gameStateMachine.CurrentState is GameHubState)
                Exit().Forget();
        }
    }
}