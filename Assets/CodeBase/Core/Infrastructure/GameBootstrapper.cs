using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.GlobalStates;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;
        private StatesFactory statesFactory;

        [Inject]
        void Construct(GameStateMachine gameStateMachine, StatesFactory statesFactory)
        {
            this.gameStateMachine = gameStateMachine;
            this.statesFactory = statesFactory;
        }
        
        private void Start()
        {
            gameStateMachine?.RegisterState(statesFactory.Create<GameBootstrapState>());
            gameStateMachine?.RegisterState(statesFactory.Create<GameLoadingState>());
            gameStateMachine?.RegisterState(statesFactory.Create<GameModeState>());

            gameStateMachine?.Enter<GameBootstrapState>();

            DontDestroyOnLoad(this);
        }

        public class Factory : PlaceholderFactory<GameBootstrapper> { }
    }
}