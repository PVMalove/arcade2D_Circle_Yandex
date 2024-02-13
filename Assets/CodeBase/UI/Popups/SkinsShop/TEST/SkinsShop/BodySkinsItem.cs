using CodeBase.UI.Popups.SkinsShop.TEST.Skins.BodySkin;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop
{
    [CreateAssetMenu(fileName = "BodySkinsItem", menuName = "Configs/UI/SkinsShop/BodyConfig", order = 1)]
    public class BodySkinsItem : ShopItemConfig
    {
        [field: SerializeField] public BodySkins SkinType { get; private set; }
    }
}