using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins;
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
        private OpenSkinsChecker openSkinsChecker;
        private SelectedSkinChecker selectedSkinChecker;

        [Inject]
        private void Construct (IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }
        
        public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
        {
            this.openSkinsChecker = openSkinsChecker;
            this.selectedSkinChecker = selectedSkinChecker;
            CreateView();
        }

        private void CreateView()
        {
            // foreach (BodySkinsItem skinItem in staticDataService.SkinsItemCatalog.BodySkinItems)
            // {
            //     SkinItemView skinItemView = Instantiate(skinBodyItemView, container);
            // }

            ShopItemVisitor visitor = new ShopItemVisitor (skinBodyItemView, skinFaceItemView);
            SkinItemFactory skinItemFactory = new SkinItemFactory(visitor);

            foreach (BodySkinsItem skinItem in staticDataService.SkinsItemCatalog.BodySkinItems)
            {
                SkinItemView skinItemView = skinItemFactory.Get(skinItem, container);
                
                openSkinsChecker.Visit(skinItemView.Item);
                if (openSkinsChecker.IsOpened)
                    skinItemView.Unlock();
                else
                    skinItemView.Lock();
                
            }
            
            foreach (FaceSkinsItem skinItem in staticDataService.SkinsItemCatalog.FaceSkinItems)
            {
                SkinItemView skinItemView = skinItemFactory.Get(skinItem, container);
                
                openSkinsChecker.Visit(skinItemView.Item);
                if (openSkinsChecker.IsOpened)
                    skinItemView.Unlock();
                else
                    skinItemView.Lock();
            }
        }
    }
}