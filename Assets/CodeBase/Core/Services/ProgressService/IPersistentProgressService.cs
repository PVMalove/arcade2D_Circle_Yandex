using CodeBase.Core.Data;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.StaticData;
using UnityEngine.AddressableAssets;

namespace CodeBase.Core.Services.ProgressService
{
    public interface IPersistentProgressService
    {
        AssetReferenceT<CircleHeroData> SelectedCircleDataReference { get; }
        void Initialize(PlayerProgress progress);
        PlayerProgress GetProgress();
        void OpenCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference);
        void SelectedCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference);
        bool IsPlayerOwnCircleHeroSkin(AssetReference circleDataReference);
    }
}