using System.Collections.Generic;
using CodeBase.StaticData.UI.Shop;

namespace CodeBase.UI.Popups.Shop
{
    public interface ISkinsShopPresenter
    {
        IReadOnlyCollection<SkinShopItem> SkinItems { get; set; }
        void InitializeShop();
    }
}