using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Data;
using CodeBase.StaticData.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Core.Services.ProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public AssetReferenceT<CircleHeroData> SelectedCircleDataReference { get; private set; }
        public int CoinsAmount => playerProgress.CoinData.CoinsAmount;
        public event Action CoinsAmountChanged;
        
        private List<AssetReferenceT<CircleHeroData>> ownedCircleHeroesReferences;
        private PlayerProgress playerProgress;

        public void Initialize(PlayerProgress progress)
        {
            playerProgress = progress;
            
            ownedCircleHeroesReferences = progress.PlayerItemsData.SkinGuids
                .Select(guid => new AssetReferenceT<CircleHeroData>(guid))
                .ToList();
            
            SelectedCircleDataReference = ownedCircleHeroesReferences.Find(
                reference => reference.AssetGUID == progress.PlayerItemsData.SelectedCircleHeroGuid);
        }
        
        public PlayerProgress GetProgress() => playerProgress;
        
        public void AddCoins(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Incorrect coins amount transferred!");
                return;
            }
            
            playerProgress.CoinData.CoinsAmount += amount;
            CoinsAmountChanged?.Invoke();
        }
        
        public void RemoveCoins(int amount)
        {
            if (!IsCoinsEnoughFor(amount))
            {
                Debug.LogError("Incorrect coins amount transferred!");
            }

            playerProgress.CoinData.CoinsAmount -= amount;
            CoinsAmountChanged?.Invoke();
        }
        
        public void OpenCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference)
        {
            if (IsPlayerOwnCircleHeroSkin(circleDataReference))
            {
                Debug.LogError($"Player already have such skin - {circleDataReference}");
                return;
            }
            
            ownedCircleHeroesReferences.Add(circleDataReference);
            playerProgress.PlayerItemsData.SkinGuids.Add(circleDataReference.AssetGUID);
        }
        
        public void SelectedCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference)
        {
            if (!IsPlayerOwnCircleHeroSkin(circleDataReference))
            {
                Debug.LogError($"Player does not own such skin- {circleDataReference}");
                return;
            }
            SelectedCircleDataReference = circleDataReference;
            playerProgress.PlayerItemsData.SelectedCircleHeroGuid = circleDataReference.AssetGUID;
        }
        
        public bool IsPlayerOwnCircleHeroSkin(AssetReference circleDataReference) => 
            playerProgress.PlayerItemsData.SkinGuids.Contains(circleDataReference.AssetGUID);
        
        public bool IsCoinsEnoughFor(int itemPrice) => CoinsAmount >= itemPrice;
    }
}