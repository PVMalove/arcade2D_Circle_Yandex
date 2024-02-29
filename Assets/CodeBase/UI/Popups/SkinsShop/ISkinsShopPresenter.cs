using System.Collections.Generic;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;

namespace CodeBase.UI.Popups.SkinsShop
{
    public interface ISkinsShopPresenter
    {
        IReadOnlyCollection<SkinShopItem> SkinItems { get; set; }
        void InitializeShop();
    }
}