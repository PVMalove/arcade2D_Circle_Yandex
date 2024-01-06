using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public abstract class StateMachine : IStateMachine
    {
        public event Action OnExitState;

        private readonly Dictionary<Type, IExitableState> registeredStates;
        private IExitableState currentState;

        public IExitableState CurrentState => currentState;

        protected StateMachine() =>
            registeredStates = new Dictionary<Type, IExitableState>();

        public async UniTask Enter<TState>() where TState : class, IState
        {
            TState newState = await ChangeState<TState>();
            await newState.Enter();
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            TState newState = await ChangeState<TState>();
            await newState.Enter(payload);
        }

        public void RegisterState<TState>(TState state) where TState : IExitableState =>
            registeredStates.Add(typeof(TState), state);

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (currentState is not null)
            {
                await currentState.Exit();
                OnExitState?.Invoke();
            }

            TState state = GetState<TState>();
            currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            registeredStates[typeof(TState)] as TState;
    }
}