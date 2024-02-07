using CodeBase.StaticData.UI.SkinsShop;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public interface IShopItemVisitor
    {
        void Visit(ShopItemConfig ShopItem);
        void Visit(BodySkinsItem skinBodyItemView);
        void Visit(FaceSkinsItem skinFaceItemView);
    }
}