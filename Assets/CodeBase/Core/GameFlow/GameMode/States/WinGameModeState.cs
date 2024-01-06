using CodeBase.Core.Infrastructure.States.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class WinGameModeState : IState
    {
        public UniTask Exit()
        {
            // use such states for showing congratulation screens and offering bonuses for ads :)
            return default;
        }

        public UniTask Enter()
        {
            return default;
        }
    }
}