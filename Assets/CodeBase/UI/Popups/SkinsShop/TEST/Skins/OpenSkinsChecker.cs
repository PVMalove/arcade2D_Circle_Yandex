using System.Linq;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.StaticData.UI.SkinsShop;

namespace CodeBase.UI.Popups.SkinsShop.TEST.Skins
{
    public class OpenSkinsChecker : IShopItemVisitor
    {
        private readonly IPersistentProgressStorage progressStorage;
        public bool IsOpened { get; private set; }

        public OpenSkinsChecker(IPersistentProgressStorage progressStorage) 
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
            => IsOpened = progressStorage.Progress.SkinData.OpenBodySkins.Contains(skinBodyItem.SkinType);

        public void Visit(FaceSkinsItem skinFaceItem) 
            => IsOpened = progressStorage.Progress.SkinData.OpenFaceSkins.Contains(skinFaceItem.SkinType);
    }
}
