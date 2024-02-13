using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop;

namespace CodeBase.UI.Popups.SkinsShop.TEST.Skins
{
    public class SelectedSkinChecker : IShopItemVisitor
    {
        private readonly IPersistentProgressStorage progressStorage;

        public bool IsSelected { get; private set; }

        public SelectedSkinChecker(IPersistentProgressStorage progressStorage) 
            => this.progressStorage = progressStorage;

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
            => IsSelected = progressStorage.Progress.SkinData.SelectedBodySkins == skinBodyItem.SkinType;

        public void Visit(FaceSkinsItem skinFaceItem) 
            => IsSelected = progressStorage.Progress.SkinData.SelectedFaceSkins == skinFaceItem.SkinType;
    }
}
