using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop
{
    public sealed class SkinsShopPresenter : ISkinsShopPresenter
    {
        public OpenSkinsChecker OpenSkinsChecker { get; set; }
        public SelectedSkinChecker SelectedSkinChecker { get; set; }
        public SkinSelector SkinSelector { get; set; }
        public SkinUnlocker SkinUnlocker { get; set; }
        
        private readonly IPersistentProgressStorage progressStorage;

        public SkinsShopPresenter(IPersistentProgressStorage progressStorage)
        {
            this.progressStorage = progressStorage;
        }

        public void InitializeShop()
        {
            OpenSkinsChecker = new OpenSkinsChecker(progressStorage);
            SelectedSkinChecker = new SelectedSkinChecker(progressStorage);
            SkinSelector = new SkinSelector(progressStorage);
            SkinUnlocker = new SkinUnlocker(progressStorage);
        }
        
        public sealed class Factory : PlaceholderFactory<ISkinsShopPresenter>
        {
        }
    }
}