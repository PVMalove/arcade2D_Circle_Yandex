using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.UI
{
    [CreateAssetMenu(fileName = "PopupsConfig", menuName = "Configs/UI/PopupsConfig")]
    public class PopupsConfig : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject skinsShopPrefabReference;

        public AssetReferenceGameObject SkinsShopPrefabReference => skinsShopPrefabReference;
    }
}