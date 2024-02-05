using CodeBase.StaticData.UI.SkinsShop;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public interface IShopItemVisitor
    {
        SkinItemView Prefab { get; }
        //void Visit(ShopItemConfig ShopItemConfig);
        void Visit(ShopItemConfig ShopItem);
        void Visit(BodySkinsItem skinBodyItemView);
        void Visit(FaceSkinsItem skinFaceItemView);
    }
}