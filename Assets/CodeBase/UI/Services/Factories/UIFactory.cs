using CodeBase.Core.Services.StaticDataService;
using CodeBase.StaticData.UI;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Popups.SkinsShop;
using CodeBase.UI.Root;
using CodeBase.UI.Windows.GameMenu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        public IUIRoot UIRoot { get; private set; }

        private readonly IStaticDataService staticDataService;
        private readonly UIRoot.Factory uiRootFactory;
        private readonly BuildInfoViewHUD.Factory buildInfoFactory;
        private readonly SettingBarViewHUD.Factory settingBarFactory;
        private readonly GameMenuViewWindow.Factory gameMenuFactory;
        private readonly SkinsShopViewPopup.Factory skinsShopFactory;

        public UIFactory(IStaticDataService staticDataService,
            UIRoot.Factory uiRootFactory,
            BuildInfoViewHUD.Factory buildInfoFactory,
            SettingBarViewHUD.Factory settingBarFactory,
            GameMenuViewWindow.Factory gameMenuFactory,
            SkinsShopViewPopup.Factory skinsShopFactory
        )
        {
            this.staticDataService = staticDataService;
            this.uiRootFactory = uiRootFactory;
            this.buildInfoFactory = buildInfoFactory;
            this.settingBarFactory = settingBarFactory;
            this.gameMenuFactory = gameMenuFactory;
            this.skinsShopFactory = skinsShopFactory;
        }

        public void CreateUIRoot() =>
            UIRoot = uiRootFactory.Create();

        public BuildInfoViewHUD CreateBuildInfoView() =>
            buildInfoFactory.Create();

        public SettingBarViewHUD CreateSettingBarView() =>
            settingBarFactory.Create();

        public UniTask<GameMenuViewWindow> CreateGameMenuView()
        {
            WindowsConfig windowsConfig = staticDataService.WindowsConfig;
            return gameMenuFactory.Create(windowsConfig.GameMenuPrefabReference);
        }

        public UniTask<SkinsShopViewPopup> CreateSkinsShopView()
        {
            PopupsConfig popupsConfig = staticDataService.PopupsConfig;
            return skinsShopFactory.Create(popupsConfig.SkinsShopPrefabReference);
        }

        public void Cleanup()
        {
        }
    }
}