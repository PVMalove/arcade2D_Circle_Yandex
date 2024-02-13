using System;
using CodeBase.UI.Popups.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.UI
{
    [CreateAssetMenu(fileName = "PopupsCatalog", menuName = "Configs/UI/PopupsCatalog")]
    public class PopupsCatalog : ScriptableObject
    {
        [Space]
        [SerializeField] private PopupInfo[] popups = Array.Empty<PopupInfo>();
        
        public AssetReferenceGameObject LoadPrefab(PopupName name)
        {
            for (int i = 0, count = popups.Length; i < count; i++)
            {
                PopupInfo info = popups[i];
                if (info.name == name)
                    return info.prefab;
            }
            throw new Exception($"Popup prefab {name} - is not found!");
        }
        
        [Serializable]
        private sealed class PopupInfo
        {
            [SerializeField]
            public PopupName name;

            [SerializeField]
            public AssetReferenceGameObject prefab;
        }
    }
}