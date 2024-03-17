using CodeBase.Gameplay.Environment;
using CodeBase.Gameplay.Player;
using CodeBase.UI.Root;
using Zenject;

namespace CodeBase.Core.Infrastructure.Factories
{
    public class GameFactoryInstaller : Installer<GameFactoryInstaller>
    {
        public override void InstallBindings()
        {
            // bind sub-factories here
            Container.BindFactory<HUDRoot, HUDRoot.Factory>().FromComponentInNewPrefabResource(InfrastructureAssetPath.HUDRoot);
            
            Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();
            
            Container.BindFactory<CircleBackground, CircleBackground.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CircleBackground)
                .WithGameObjectName("CircleBackground");
            
            Container.Bind<CircleHeroView.Factory>().AsSingle();
            Container.Bind<CircleHero.Factory>().AsSingle();
        }
    }
}