using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.InputService;
using CodeBase.Gameplay.Player;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.GameFlow.GameMode
{
    public class GameModeSceneInstaller : MonoInstaller
    {
        // Here we bind dependencies that make sense only in gameplay scene.
        // If we need some dependencies from scene for our game mode
        // we can link it on scene right here for binding and use it in scene context
        
        public override void InstallBindings()
        {
            Debug.Log($"[Installer] -> [GameModeScene] -> Start game scene installer");
            
            Container.BindInterfacesAndSelfTo<GameModeSceneBootstraper>().AsSingle().NonLazy(); // non lazy due to it's not injected anywhere but we still need to instanciate it
            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();
            Container.Bind<SceneStateMachine>().AsSingle();
            
            UIInstaller.Install(Container);
            GameWorldInstaller.Install(Container);
            
            BindInputService();
        }
        
        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .FromSubContainerResolve()
                .ByInstaller<InputInstaller>()
                .AsSingle();
        }
    }
}