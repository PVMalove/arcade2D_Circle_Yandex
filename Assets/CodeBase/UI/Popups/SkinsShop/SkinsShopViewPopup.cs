using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popups.SkinsShop
{
    public class SkinsShopViewPopup : PopupBase<ISkinsShopPresenter>
    {
        [SerializeField] private Button closePopupButton;
        [SerializeField] private ShopItemsPresenter shopItemList;
        
        private ISkinsShopPresenter presenter;

        protected override async void OnAwake()
        {
            base.OnAwake();
            await shopItemList.InitializeAsync();
        }

        protected override async void Initialize(ISkinsShopPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            this.presenter.InitializeShop();
            await shopItemList.SetSkinItems(presenter.SkinItems);
            Debug.Log("Initialize");
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            
            closePopupButton.onClick.AddListener(OnClosePopup);
            Debug.Log("SubscribeUpdates");
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            shopItemList.Cleanup();
            closePopupButton.onClick.RemoveListener(OnClosePopup);
            Debug.Log("UnsubscribeUpdates");
        }

        private void OnClosePopup() => 
            SetPopupResult();
        
        [Button]
        private void OnClosePopupClick() => 
            SetPopupResult();
        
        public sealed class Factory : AddressablePrefabFactory<SkinsShopViewPopup> { }
    }
}