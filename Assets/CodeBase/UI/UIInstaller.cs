using CodeBase.UI.HUD.Service;
using CodeBase.UI.HUD.Supplier;
using CodeBase.UI.Popups.Service;
using CodeBase.UI.Popups.Supplier;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Windows.Service;
using CodeBase.UI.Windows.Supplier;
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
            Container.BindInterfacesAndSelfTo<PopupSupplier>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupSupplierAsync>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowSupplier>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowSupplierAsync>().AsSingle();

            Container.BindInterfacesAndSelfTo<HUDService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowService>().AsSingle();
        }
    }
}