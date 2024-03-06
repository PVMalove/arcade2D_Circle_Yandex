using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Data;
using CodeBase.StaticData.Level;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Core.Services.ProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public AssetReferenceT<CircleHeroData> SelectedCircleDataReference { get; private set; }

        private List<AssetReferenceT<CircleHeroData>> ownedCircleHeroesReferences;
        private PlayerProgress playerProgress;

        public void Initialize(PlayerProgress progress)
        {
            playerProgress = progress;
            
            ownedCircleHeroesReferences = progress.PlayerItems.SkinGuids
                .Select(guid => new AssetReferenceT<CircleHeroData>(guid))
                .ToList();
            
            SelectedCircleDataReference = ownedCircleHeroesReferences.Find(
                reference => reference.AssetGUID == progress.SelectedCircleHeroGuid);
        }
        
        public PlayerProgress GetProgress() => playerProgress;
        
        public void OpenCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference)
        {
            if (IsPlayerOwnCircleHeroSkin(circleDataReference))
            {
                Debug.LogError($"Player already have such skin - {circleDataReference}");
                return;
            }
            
            ownedCircleHeroesReferences.Add(circleDataReference);
            playerProgress.PlayerItems.SkinGuids.Add(circleDataReference.AssetGUID);
        }
        
        public void SelectedCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference)
        {
            if (!IsPlayerOwnCircleHeroSkin(circleDataReference))
            {
                Debug.LogError($"Player does not own such skin- {circleDataReference}");
                return;
            }
            SelectedCircleDataReference = circleDataReference;
            playerProgress.SelectedCircleHeroGuid = circleDataReference.AssetGUID;
        }
        
        public bool IsPlayerOwnCircleHeroSkin(AssetReference circleDataReference) => 
            playerProgress.PlayerItems.SkinGuids.Contains(circleDataReference.AssetGUID);
        
    }
}