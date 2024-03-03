using Code.Infrastructure.Services.Pool;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2.Pool
{
    [CreateAssetMenu(fileName = "NewName_PoolObjectData", menuName = "Configs/Infrastructure/Pool/PoolObjectData")]
    public class PoolObjectConfig : ScriptableObject
    {
        [SerializeField] private PoolObjectType type;
        [SerializeField] private int startCapacity;
        [SerializeField] private AssetReferenceGameObject assetReference;

        public PoolObjectType Type => type;
        public int StartCapacity => startCapacity;
        public AssetReferenceGameObject AssetReference => assetReference;
    }
}