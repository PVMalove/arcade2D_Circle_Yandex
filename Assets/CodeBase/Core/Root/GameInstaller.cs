using CodeBase.Audio.Core;
using CodeBase.Audio.Service;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.UI.AwaitingOverlay;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.PauseService;
using CodeBase.Core.Services.PoolService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.RandomizerService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Core.Root
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameBootstraperFactory();

            BindSceneLoader();

            BindInfrastructureUI();

            BindGameStateMachine();

            BindStaticDataService();
            
            BindGameFactory();

            BindRandomizeService();

            BindPlayerProgressService();

            BindLoadService();
            
            BindSaveService();

            BindLogService();

            BindAssetProvider();

            BindConfigLoader();

            BindAudioManagement();

            BindLoadingAudioService();

            BindPauseService();
            
            Container.Bind<PoolFactory>().AsSingle(); //TODO REFACTOR
        }

        private void BindAudioManagement()
        {
            Container.BindInterfacesTo<AudioManagement>().AsSingle();
        }

        private void BindAssetProvider() =>
            Container.BindInterfacesTo<AssetProvider>().AsSingle();

        private void BindConfigLoader()
        {
            Container
                .Bind<ConfigLoader>()
                .FromScriptableObjectResource("Configs/ConfigLoader")
                .AsTransient()
                .WhenInjectedInto<AssetProvider>();
        }

        private void BindLogService() =>
            Container.BindInterfacesTo<LogService>().AsSingle();

        private void BindStaticDataService() =>
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

        private void BindGameBootstraperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstraperPath)
                .WithGameObjectName("GameBootstrapper");
        }

        private void BindSaveService() =>
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();

        private void BindLoadService() =>
            Container.BindInterfacesAndSelfTo<LoadService>().AsSingle();

        private void BindPlayerProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<PersistentProgressService>()
                .AsSingle();
        }

        private void BindRandomizeService() =>
            Container.BindInterfacesAndSelfTo<RandomizerService>().AsSingle();

        private void BindGameFactory()
        {
            Container
                .Bind<IGameFactory>()
                .FromSubContainerResolve()
                .ByInstaller<GameFactoryInstaller>()
                .AsSingle();
        }

        private void BindSceneLoader() =>
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

        private void BindInfrastructureUI()
        {
            BindLoadingCurtains();
            BindAwaitingOverlay();
        }

        private void BindLoadingCurtains()
        {
            Container.BindFactory<string, UniTask<LoadingCurtain>, LoadingCurtain.Factory>()
                .FromFactory<PrefabFactoryAsync<LoadingCurtain>>();

            Container.BindInterfacesAndSelfTo<LoadingCurtainProxy>().AsSingle();
        }
        
        private void BindAwaitingOverlay()
        {
            Container
                .BindFactory<string, UniTask<AwaitingOverlay>, AwaitingOverlay.Factory>()
                .FromFactory<PrefabFactoryAsync<AwaitingOverlay>>();

            Container.BindInterfacesAndSelfTo<AwaitingOverlayProxy>().AsSingle();
        }

        private void BindLoadingAudioService()
        {
            Container.BindFactory<AudioService, AudioService.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.AudioServicePath)
                .WithGameObjectName("AudioService");
            Container.BindInterfacesAndSelfTo<AudioServiceProxy>().AsSingle();
        }

        private void BindGameStateMachine() =>
            GameStateMachineInstaller.Install(Container);

        private void BindPauseService()
        {
            Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
        }
    }
}