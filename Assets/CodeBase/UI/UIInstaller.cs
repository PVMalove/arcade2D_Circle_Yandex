using CodeBase.UI.HUD.Service;
using CodeBase.UI.HUD.Supplier;
using CodeBase.UI.Popups.Service;
using CodeBase.UI.Popups.Supplier;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Window;
using Zenject;

namespace CodeBase.UI
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            UIFactoryInstaller.Install(Container);
            UIFactoryPresenterInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<HUDSupplier>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupSupplierAsync>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<WindowService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupService>().AsSingle();
            Container.BindInterfacesAndSelfTo<HUDService>().AsSingle();
        }
    }
}