using System;
using CodeBase.StaticData.UI.SkinsShop;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public class ShopItemVisitor : IShopItemVisitor
    {
        private SkinItemView skinBodyItemView;
        private SkinItemView skinFaceItemView;

        public ShopItemVisitor(SkinItemView skinBodyItemView, SkinItemView skinFaceItemView)
        {
            this.skinBodyItemView = skinBodyItemView;
            this.skinFaceItemView = skinFaceItemView;
        }

        public SkinItemView Prefab { get; private set; }

        // public void Visit(ShopItemConfig ShopItemConfig) => Visit((dynamic)ShopItemConfig);

        public void Visit(ShopItemConfig ShopItem)
        {
            switch (ShopItem)
            {
                case BodySkinsItem skinBodyItem:     
                    Visit(skinBodyItem); break;
                case FaceSkinsItem skinFaceItem:     
                    Visit(skinFaceItem); break;
            }
        }

        public void Visit(BodySkinsItem skinBodyItem) => Prefab = skinBodyItemView;

        public void Visit(FaceSkinsItem skinFaceItem) => Prefab = skinFaceItemView;
    }
}