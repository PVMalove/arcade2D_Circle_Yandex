using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.GameFlow.GameHUD
{
    public class GameHUDSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log($"[Installer] -> [GameHUDScene] -> Start loading scene installer");
            Container.BindInterfacesAndSelfTo<GameHUDSceneBootstraper>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();
            Container.Bind<SceneStateMachine>().AsSingle();
            UIInstaller.Install(Container);
        }
    }

}