using System.Collections.Generic;
using CodeBase.StaticData.UI.SkinsShop;
using UnityEngine;

namespace CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop
{
    [CreateAssetMenu(fileName = "SkinsBodyItemCatalog", menuName = "Configs/UI/SkinsShop/SkinsBodyItemCatalog", order = 0)]
    public class SkinsItemCatalog : ScriptableObject
    {
        [SerializeField] private List<BodySkinsItem> bodySkinItems;
        [SerializeField] private List<FaceSkinsItem> faceSkinItems;
        public IEnumerable<BodySkinsItem> BodySkinItems => bodySkinItems;
        public IEnumerable<FaceSkinsItem> FaceSkinItems => faceSkinItems;
    }
}