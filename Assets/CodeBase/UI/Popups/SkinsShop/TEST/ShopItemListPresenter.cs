using CodeBase.Core.Services.StaticDataService;
using CodeBase.StaticData.UI.SkinsShop;
using CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public class ShopItemListPresenter : MonoBehaviour
    {
        [SerializeField] private SkinItemView skinBodyItemView;
        [SerializeField] private SkinItemView skinFaceItemView;
        [SerializeField] private Transform container;
        [SerializeField] private SkinsItemCatalog catalog;
        
        private IStaticDataService staticDataService;

        [Inject]
        private void Construct (IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }

        private void OnEnable()
        {
            SkinItemFactory skinItemFactory = new SkinItemFactory();
            IShopItemVisitor visitor = new ShopItemVisitor (skinBodyItemView, skinFaceItemView);
            
            foreach (BodySkinsItem skinItem in staticDataService.SkinsItemCatalog.BodySkinItems)
            {
                SkinItemView skinItemView = skinItemFactory.Get(visitor, skinItem, container);
            }
            
            foreach (FaceSkinsItem skinItem in staticDataService.SkinsItemCatalog.FaceSkinItems)
            {
                SkinItemView skinItemView = skinItemFactory.Get(visitor, skinItem, container);
            }
        }
    }
}