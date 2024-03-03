using CodeBase.Core.Infrastructure.States.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class FailGameModeState : IState
    {
        public UniTask Enter()
        {
            // use such states for showing fail screens and offering resurrections and so on
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}