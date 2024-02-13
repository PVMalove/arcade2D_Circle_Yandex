using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop;

namespace CodeBase.UI.Popups.SkinsShop.TEST.Skins
{
    public class SkinUnlocker : IShopItemVisitor
    {
        private readonly IPersistentProgressStorage progressStorage;

        public SkinUnlocker(IPersistentProgressStorage progressStorage) 
            => this.progressStorage = progressStorage;

        // public void Visit(ShopItemConfig shopItem) => Visit((dynamic)shopItem);
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

        public void Visit(BodySkinsItem skinBodyItem)
            => progressStorage.Progress.SkinData.OpenBodySkin(skinBodyItem.SkinType);

        public void Visit(FaceSkinsItem skinFaceItem) 
            => progressStorage.Progress.SkinData.OpenFaceSkin(skinFaceItem.SkinType);
    }
}
