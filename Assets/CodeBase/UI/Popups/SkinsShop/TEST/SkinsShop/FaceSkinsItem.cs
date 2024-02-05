using CodeBase.UI.Popups.SkinsShop.Skins.FaceSkin;
using UnityEngine;

namespace CodeBase.StaticData.UI.SkinsShop
{
    [CreateAssetMenu(fileName = "FaceSkinsItem", menuName = "Configs/UI/SkinsShop/FaceConfig", order = 2)]
    public class FaceSkinsItem : ShopItemConfig
    {
        [field: SerializeField] public FaceSkins SkinType { get; private set; }
    }
}