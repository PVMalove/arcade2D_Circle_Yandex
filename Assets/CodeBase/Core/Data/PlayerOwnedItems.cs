using System;
using System.Collections.Generic;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.StaticData;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerOwnedItems
    {
        public string SelectedCircleHeroGuid;
        public List<string> SkinGuids;
        
        private List<AssetReferenceT<CircleHeroData>> ownedCircleHeroesReferences = new List<AssetReferenceT<CircleHeroData>>();
        
        public AssetReferenceT<CircleHeroData> SelectedCircleHeroDataReference { get; private set; }
        public IReadOnlyCollection<AssetReferenceT<CircleHeroData>> OwnedCircleHeroesReferences => ownedCircleHeroesReferences;
        
        public PlayerOwnedItems(List<string> SkinGuids)
        {
            this.SkinGuids = SkinGuids;
        }
        
        public void OpenCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference)
        {
            if (IsPlayerOwnCircleHeroSkin(circleDataReference))
            {
                Debug.LogError($"Player already have such skin - {circleDataReference}");
                return;
            }
            
            ownedCircleHeroesReferences.Add(circleDataReference);
            SkinGuids.Add(circleDataReference.AssetGUID);
        }
        
        public void SelectedCircleHeroSkin(AssetReferenceT<CircleHeroData> circleDataReference)
        {
            if (!IsPlayerOwnCircleHeroSkin(circleDataReference))
            {
                Debug.LogError($"Player does not own such skin- {circleDataReference}");
                return;
            }
            SelectedCircleHeroDataReference = circleDataReference;
            SelectedCircleHeroGuid = circleDataReference.AssetGUID;
        }
        
        public bool IsPlayerOwnCircleHeroSkin(AssetReference circleDataReference) => 
            SkinGuids.Contains(circleDataReference.AssetGUID);
    }
}