using CodeBase.UI.Root;
using UnityEngine;
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
        }
    }
}