using CodeBase.UI.Popups.SkinsShop.TEST.Skins.FaceSkin;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop
{
    [CreateAssetMenu(fileName = "FaceSkinsItem", menuName = "Configs/UI/SkinsShop/FaceConfig", order = 2)]
    public class FaceSkinsItem : ShopItemConfig
    {
        [field: SerializeField] public FaceSkins SkinType { get; private set; }
    }
}