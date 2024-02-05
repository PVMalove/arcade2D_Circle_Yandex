using CodeBase.Core.Services.StaticDataService;
using CodeBase.StaticData.UI.SkinsShop;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public sealed class SkinItemFactory
    {
        public SkinItemView Get(IShopItemVisitor visitor, ShopItemConfig shopItem,
            Transform container)
        {
            visitor.Visit(shopItem);
            SkinItemView instance = Object.Instantiate(visitor.Prefab, container);
            instance.SetSkin(shopItem.Image);
            return instance;
        }
    }
}