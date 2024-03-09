using System;
using System.Collections.Generic;
using CodeBase.StaticData.UI.Shop;

namespace CodeBase.UI.Popups.Shop
{
    public interface ISkinsShopPresenter
    {
        event Action ChangedCoinsAmount;
        string CoinsAmount { get; }
        IReadOnlyCollection<SkinShopItem> SkinItems { get; set; }
        void InitializeShopItems();
        void Subscribe();
        void Unsubscribe();
    }
}