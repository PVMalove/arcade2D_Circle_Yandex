using CodeBase.Core.Infrastructure;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop;
using CodeBase.UI.Root;
using CodeBase.UI.Services.Infrastructure;
using CodeBase.UI.Windows.GameMenu;
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
            
            //POPUP
            Container.Bind<SkinsShopViewPopup.Factory>().AsSingle();
            
            //HUD 
            Container.BindFactory<BuildInfoViewHUD, BuildInfoViewHUD.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.BuildInfoPath)
                .WithGameObjectName("BuildInfo");
            
            Container.BindFactory<SettingBarViewHUD, SettingBarViewHUD.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.SettingBar)
                .WithGameObjectName("SettingBar");
            
            //WINDOW
            Container.Bind<GameMenuViewScreen.Factory>().AsSingle();
        }
    }
}