using System.Collections.Generic;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop
{
    public sealed class SkinsShopPresenter : ISkinsShopPresenter
    {
        public IReadOnlyCollection<SkinShopItem> SkinItems{ get; set; }
        
        private readonly IPersistentProgressStorage progressStorage;
        private readonly IStaticDataService staticDataService;

        public SkinsShopPresenter(IPersistentProgressStorage progressStorage, IStaticDataService staticDataService)
        {
            this.progressStorage = progressStorage;
            this.staticDataService = staticDataService;
        }

        public void InitializeShop()
        {
            SkinItems = staticDataService.ShopItemsCatalog.SkinItems;
        }
        
        public sealed class Factory : PlaceholderFactory<ISkinsShopPresenter>
        {
        }
    }
}