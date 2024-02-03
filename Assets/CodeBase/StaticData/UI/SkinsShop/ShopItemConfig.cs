using UnityEngine;

namespace CodeBase.StaticData.UI.SkinsShop
{
    public class ShopItemConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField, Range(0, 1000)] public int Price { get; private set; }
    }
}