using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Level
{
    
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/Character/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject prefabReference;
    }
}