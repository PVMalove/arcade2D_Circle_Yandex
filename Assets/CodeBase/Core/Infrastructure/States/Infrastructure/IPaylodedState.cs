using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public interface IPaylodedState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}