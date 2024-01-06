using CodeBase.Core.Infrastructure.States.Infrastructure;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Infrastructure.States
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log($"[Installer] -> [GameStateMachine] -> Game stateMachine installer");
            Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}