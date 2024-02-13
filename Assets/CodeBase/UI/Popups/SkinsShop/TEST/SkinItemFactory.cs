using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public sealed class SkinItemFactory
    {
        private readonly ShopItemVisitor visitor;

        public SkinItemFactory(ShopItemVisitor visitor)
        {
            this.visitor = visitor;
        }

        public SkinItemView Get(ShopItemConfig shopItem, Transform container)
        {
            visitor.Visit(shopItem);
            SkinItemView instance = Object.Instantiate(visitor.Prefab, container);
            
            instance.SetSkin(shopItem.Image);
            instance.Initialize(shopItem);
            return instance;
        }
    }
}