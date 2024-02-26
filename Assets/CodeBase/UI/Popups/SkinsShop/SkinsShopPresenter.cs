using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop
{
    public sealed class SkinsShopPresenter : ISkinsShopPresenter
    {
        public ShopItemsCatalog ShopItemsCatalog { get; set; }
        // public OpenSkinsChecker OpenSkinsChecker { get; set; }
        // public SelectedSkinChecker SelectedSkinChecker { get; set; }
        // public SkinSelector SkinSelector { get; set; }
        // public SkinUnlocker SkinUnlocker { get; set; }
        
        private readonly IPersistentProgressStorage progressStorage;
        private readonly IStaticDataService staticDataService;

        public SkinsShopPresenter(IPersistentProgressStorage progressStorage, IStaticDataService staticDataService)
        {
            this.progressStorage = progressStorage;
            this.staticDataService = staticDataService;
        }

        public void InitializeShop()
        {
            ShopItemsCatalog = staticDataService.ShopItemsCatalog;
            // OpenSkinsChecker = new OpenSkinsChecker(progressStorage);
            // SelectedSkinChecker = new SelectedSkinChecker(progressStorage);
            // SkinSelector = new SkinSelector(progressStorage);
            // SkinUnlocker = new SkinUnlocker(progressStorage);

        }
        
        public sealed class Factory : PlaceholderFactory<ISkinsShopPresenter>
        {
        }
    }
}