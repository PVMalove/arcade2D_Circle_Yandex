using System;
using CodeBase.UI.Windows.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.UI.Catalog
{
    [CreateAssetMenu(fileName = "ScreenCatalog", menuName = "Configs/UI/ScreenCatalog")]
    public class ScreensCatalog : ScriptableObject
    {
        [Space]
        [SerializeField] private ScreenInfo[] screens = Array.Empty<ScreenInfo>();
        
        public AssetReferenceGameObject LoadPrefab(ScreenName name)
        {
            for (int i = 0, count = screens.Length; i < count; i++)
            {
                ScreenInfo info = screens[i];
                if (info.name == name)
                    return info.prefab;
            }
            throw new Exception($"Popup prefab {name} - is not found!");
        }
        
        [Serializable]
        private sealed class ScreenInfo
        {
            [SerializeField]
            public ScreenName name;

            [SerializeField]
            public AssetReferenceGameObject prefab;
        }
    }
}