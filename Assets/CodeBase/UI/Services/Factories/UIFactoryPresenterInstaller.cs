using CodeBase.Core.GameFlow;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Windows.GameMenu;
using Zenject;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactoryPresenterInstaller : Installer<UIFactoryPresenterInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<BuildInfoConfig, IBuildInfoPresenter, BuildInfoPresenter.Factory>()
                .To<BuildInfoPresenter>();

            Container
                .BindFactory<ISettingBarPresenter, SettingBarPresenter.Factory>()
                .To<SettingBarPresenter>();
            
            Container
                .BindFactory<IGameMenuPresenter, GameMenuPresenter.Factory>()
                .To<GameMenuPresenter>();
        }
    }
}