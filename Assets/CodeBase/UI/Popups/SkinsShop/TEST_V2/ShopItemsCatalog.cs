﻿using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2
{
    [CreateAssetMenu(fileName = "ShopItemCatalog", menuName = "Configs/UI/SkinsShop/ShopItemCatalog", order = 0)]
    public class ShopItemsCatalog : ScriptableObject
    {
        [SerializeField] private List<SkinShopItem> skinItems;
        
        public IReadOnlyCollection<SkinShopItem> SkinItems => skinItems;
    }
}