using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public interface IStateMachine
    {
        UniTask Enter<TState>() where TState : class, IState;
        UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>;
        void RegisterState<TState>(TState state) where TState : IExitableState;
        event Action OnExitState;
        IExitableState CurrentState { get; }
    }
}