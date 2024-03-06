using CodeBase.StaticData.Level;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Infrastructure
{
    [CreateAssetMenu(fileName = "FirstSaveData", menuName = "Configs/Infrastructure/FirstSaveData")]
    public class FirstSaveData : ScriptableObject
    {
        [SerializeField] private AssetReferenceT<CircleHeroData> circleHeroDataReference;

        [SerializeField] private float audioVolume;
        [SerializeField] private bool musicOn;
        [SerializeField] private bool effectsOn;

        public string circleHeroGUID => circleHeroDataReference.AssetGUID;
        public float AudioVolume => audioVolume;
        public bool MusicOn => musicOn;
        public bool EffectsOn => effectsOn;
    }
}