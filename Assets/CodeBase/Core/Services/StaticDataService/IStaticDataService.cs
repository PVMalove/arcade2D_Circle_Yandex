using CodeBase.StaticData.Level;
using CodeBase.StaticData.UI;
using CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        CharacterConfig CharacterConfig { get; }
        ScreensCatalog ScreensCatalog { get; }
        PopupsCatalog PopupsCatalog { get; }
        SkinsItemCatalog SkinsItemCatalog { get; }
    }
}