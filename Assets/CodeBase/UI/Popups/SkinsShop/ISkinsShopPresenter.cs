using System.Collections.Generic;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins;

namespace CodeBase.UI.Popups.SkinsShop
{
    public interface ISkinsShopPresenter
    {
        IReadOnlyCollection<SkinShopItem> SkinItems { get; set; }

        // OpenSkinsChecker OpenSkinsChecker { get; set; }
        // SelectedSkinChecker SelectedSkinChecker { get; set;}
        // SkinSelector SkinSelector { get; set;}
        // SkinUnlocker SkinUnlocker { get; set;}
        void InitializeShop();
    }
}