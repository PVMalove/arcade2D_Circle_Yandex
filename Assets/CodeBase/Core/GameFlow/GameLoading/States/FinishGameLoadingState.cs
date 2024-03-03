using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.GlobalStates;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameLoading.States
{
    public class FinishGameLoadingState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly ILogService log;

        public FinishGameLoadingState(GameStateMachine gameStateMachine, ILogService log)
        {
            this.gameStateMachine = gameStateMachine;
            this.log = log;
        }

        public UniTask Enter()
        {
            log.LogState("Enter", this);
            gameStateMachine.OnExitState += HandleExitState;
            gameStateMachine.Enter<GameModeState>().Forget();
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            gameStateMachine.OnExitState -= HandleExitState;
            log.LogState("Exit", this);
            return UniTask.CompletedTask;
        }
        
        private void HandleExitState()
        {
            if(gameStateMachine.CurrentState is GameModeState) 
                Exit().Forget();
        }
    }
}