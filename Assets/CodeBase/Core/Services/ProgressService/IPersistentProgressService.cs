using System;
using CodeBase.Core.Data;
using CodeBase.StaticData.Level;
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
        bool IsCoinsEnoughFor(int itemPrice);
        void AddCoins(int amount);
        void RemoveCoins(int amount);
        event Action CoinsAmountChanged;
    }
}