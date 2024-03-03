using Code.Infrastructure.Services.Pool;
using CodeBase.StaticData.UI;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.Pool;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        ScreensCatalog ScreensCatalog { get; }
        PopupsCatalog PopupsCatalog { get; }
        PoolObjectConfig GetPoolConfigByType(PoolObjectType type);
        ShopItemsCatalog ShopItemsCatalog { get; } 
    }
}