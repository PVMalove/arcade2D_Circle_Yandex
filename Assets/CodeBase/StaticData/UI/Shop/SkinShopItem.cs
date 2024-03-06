﻿using CodeBase.StaticData.Level;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.UI.Shop
{
    [CreateAssetMenu(fileName = "SkinShopItem", menuName = "Configs/UI/SkinsShop/SkinShopItem", order = 1)]
    public class SkinShopItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        
        [PreviewField] 
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        
        [field: SerializeField, Range(0, 1000)] public int RequiredCoins { get; private set; }
        [field: SerializeField] public AssetReferenceT<CircleHeroData> CircleHeroReference { get; private set; }
    }
}