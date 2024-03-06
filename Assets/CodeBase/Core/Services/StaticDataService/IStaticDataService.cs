using CodeBase.Core.Services.PoolService;
using CodeBase.StaticData.UI;
using CodeBase.StaticData.UI.Catalog;
using CodeBase.StaticData.UI.Shop;
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