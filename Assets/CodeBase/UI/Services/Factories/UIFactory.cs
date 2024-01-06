using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        public IUIRoot UIRoot { get; private set; }

        private readonly UIRoot.Factory uiRootFactory;
        private readonly BuildInfoViewHUD.Factory buildInfoFactory;
        private readonly SettingBarViewHUD.Factory settingBarFactory;

        public UIFactory(UIRoot.Factory uiRootFactory,
            BuildInfoViewHUD.Factory buildInfoFactory,
            SettingBarViewHUD.Factory settingBarFactory
        )
        {
            this.uiRootFactory = uiRootFactory;
            this.buildInfoFactory = buildInfoFactory;
            this.settingBarFactory = settingBarFactory;
        }

        public void CreateUIRoot() =>
            UIRoot = uiRootFactory.Create();

        public BuildInfoViewHUD CreateBuildInfoView() =>
            buildInfoFactory.Create();

        public SettingBarViewHUD CreateSettingBarView() =>
            settingBarFactory.Create();

        public void Cleanup()
        {
        }
    }
}