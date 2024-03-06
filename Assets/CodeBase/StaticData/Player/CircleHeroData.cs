using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(fileName = "CircleHeroData", menuName = "Configs/CircleHero/CircleHeroData", order = 0)]
    public class CircleHeroData : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject prefab;
        public AssetReferenceGameObject Prefab => prefab;
    }
}