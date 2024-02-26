using CodeBase.UI.Popups.SkinsShop.TEST_V2.StaticData;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2
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