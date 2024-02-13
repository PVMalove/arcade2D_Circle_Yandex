using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop
{
    public class ShopItemConfig : ScriptableObject
    {
        [PreviewField] 
        [SerializeField] private Sprite image;

        [field: SerializeField, Range(0, 1000)] public int Price { get; private set; }
        
        public Sprite Image => image;
    }
}