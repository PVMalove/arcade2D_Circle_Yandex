using System;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.StaticData.UI;
using CodeBase.StaticData.UI.Catalog;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.Shop;
using CodeBase.UI.Root;
using CodeBase.UI.Windows.Base;
using CodeBase.UI.Windows.GameMenu;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace CodeBase.UI.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        public IUIRoot UIRoot { get; private set; }

        private readonly IStaticDataService staticDataService;
        private readonly UIRoot.Factory uiRootFactory;
        private readonly BuildInfoViewHUD.Factory buildInfoFactory;
        private readonly SettingBarViewHUD.Factory settingBarFactory;
        private readonly GameMenuViewScreen.Factory gameMenuFactory;
        private readonly SkinsShopViewPopup.Factory skinsShopFactory;

        public UIFactory(IStaticDataService staticDataService,
            UIRoot.Factory uiRootFactory,
            BuildInfoViewHUD.Factory buildInfoFactory,
            SettingBarViewHUD.Factory settingBarFactory,
            GameMenuViewScreen.Factory gameMenuFactory,
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

        public UniTask<GameMenuViewScreen> CreateGameMenuView()
        {
            ScreensCatalog screensCatalog = staticDataService.ScreensCatalog;
            AssetReferenceGameObject prefab = screensCatalog.LoadPrefab(ScreenName.GAME_MENU);
            return gameMenuFactory.Create(prefab);
        }

        public UniTask<SkinsShopViewPopup> CreateSkinsShopView()
        {
            PopupsCatalog popupsCatalog = staticDataService.PopupsCatalog;
            AssetReferenceGameObject prefab = popupsCatalog.LoadPrefab(PopupName.SKINS_SHOP);
            return skinsShopFactory.Create(prefab);
        }

        public void Cleanup()
        {
        }
    }
}