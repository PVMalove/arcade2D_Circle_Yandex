using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.States.Infrastructure
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}