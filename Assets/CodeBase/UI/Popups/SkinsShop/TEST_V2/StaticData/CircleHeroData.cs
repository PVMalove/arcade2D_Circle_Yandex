using CodeBase.UI.Popups.SkinsShop.TEST_V2.Gameplay;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2.StaticData
{
    [CreateAssetMenu(fileName = "CircleHeroData", menuName = "Configs/CircleHero/CircleHeroData", order = 0)]
    public class CircleHeroData : ScriptableObject
    {
        [SerializeField] private CircleHero prefab;
        public CircleHero Prefab => prefab;
    }
}