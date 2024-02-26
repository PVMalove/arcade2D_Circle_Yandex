using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins;

namespace CodeBase.UI.Popups.SkinsShop
{
    public interface ISkinsShopPresenter
    {
        ShopItemsCatalog ShopItemsCatalog { get; set; }
        // OpenSkinsChecker OpenSkinsChecker { get; set; }
        // SelectedSkinChecker SelectedSkinChecker { get; set;}
        // SkinSelector SkinSelector { get; set;}
        // SkinUnlocker SkinUnlocker { get; set;}
        void InitializeShop();
    }
}