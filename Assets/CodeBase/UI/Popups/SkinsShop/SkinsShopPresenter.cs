using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop
{
    public sealed class SkinsShopPresenter : ISkinsShopPresenter
    {
        public IReadOnlyCollection<SkinShopItem> SkinItems{ get; set; }
        
        private readonly IPersistentProgressService progressService;
        private readonly IStaticDataService staticDataService;

        public SkinsShopPresenter(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            this.progressService = progressService;
            this.staticDataService = staticDataService;
        }

        public void InitializeShop()
        {
            SkinItems = staticDataService.ShopItemsCatalog.SkinItems;
        }
        
        public sealed class Factory : PlaceholderFactory<ISkinsShopPresenter> { }
    }
}