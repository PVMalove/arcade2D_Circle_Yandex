using CodeBase.Core.GameFlow.GameMode.GameWorld;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Services.RandomizerService;
using CodeBase.Core.Services.SaveLoadService;
using Zenject;

namespace CodeBase.Core.GameFlow.GameMode
{
    public class GameWorldInstaller : Installer<GameWorldInstaller>
    {
        public override void InstallBindings()
        {
            GameFactoryInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InitializeGameWorld>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizerService>().AsSingle();
            Container.Bind<PlayerStateMachine>().AsSingle();
            BindLoadingAutoSaveData();
        }
        
        private void BindLoadingAutoSaveData()
        {
            Container.BindFactory<AutoSaveData, AutoSaveData.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.AutoSaveServicePath)
                .WithGameObjectName("AutoSaveData");
        }
    }
}