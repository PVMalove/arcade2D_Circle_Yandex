using CodeBase.UI.Popups.SkinsShop.Skins.BodySkin;
using UnityEngine;

namespace CodeBase.StaticData.UI.SkinsShop
{
    [CreateAssetMenu(fileName = "BodySkinsItem", menuName = "Configs/UI/SkinsShop/BodyConfig", order = 0)]
    public class BodySkinsItem : ShopItemConfig
    {
        [field: SerializeField] public BodySkins SkinType { get; private set; }
    }
}