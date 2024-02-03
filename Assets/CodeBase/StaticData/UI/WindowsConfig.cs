using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.UI
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/UI/WindowsConfig")]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject gameMenuPrefabReference;

        public AssetReferenceGameObject GameMenuPrefabReference => gameMenuPrefabReference;
    }
}