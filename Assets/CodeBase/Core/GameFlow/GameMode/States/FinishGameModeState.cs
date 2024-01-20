using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.GlobalStates;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class FinishGameModeState : IState
    {
        private readonly GameStateMachine gameStateMachine;

        public FinishGameModeState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public UniTask Enter()
        {
            return default;
        }

        public async UniTask Exit()
        {
            // use such states for finishing gameplay and cleanup resources, posting session statistics and leaving Game State
        }
    }
}