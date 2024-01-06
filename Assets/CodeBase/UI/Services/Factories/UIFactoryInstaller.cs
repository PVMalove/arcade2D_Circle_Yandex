using CodeBase.Core.Infrastructure;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;
using Zenject;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactoryInstaller : Installer<UIFactoryInstaller>
    {
        public override void InstallBindings()
        {
            // bind ui factories here
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

            Container.BindFactory<UIRoot, UIRoot.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.UIRoot)
                .WithGameObjectName("GameUICanvas");
            
            //Popup async

            //HUD 
            Container.BindFactory<BuildInfoViewHUD, BuildInfoViewHUD.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.BuildInfoPath)
                .WithGameObjectName("BuildInfo");
            
            Container.BindFactory<SettingBarViewHUD, SettingBarViewHUD.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.SettingBar)
                .WithGameObjectName("SettingBar");
        }
    }
}