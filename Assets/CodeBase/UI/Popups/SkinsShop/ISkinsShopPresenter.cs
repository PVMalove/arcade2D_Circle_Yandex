using CodeBase.UI.Popups.SkinsShop.TEST.Skins;

namespace CodeBase.UI.Popups.SkinsShop
{
    public interface ISkinsShopPresenter
    {
        OpenSkinsChecker OpenSkinsChecker { get; set; }
        SelectedSkinChecker SelectedSkinChecker { get; set;}
        SkinSelector SkinSelector { get; set;}
        SkinUnlocker SkinUnlocker { get; set;}
        void InitializeShop();
    }
}