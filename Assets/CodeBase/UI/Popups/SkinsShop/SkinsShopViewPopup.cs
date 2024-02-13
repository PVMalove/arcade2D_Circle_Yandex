using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop.TEST;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popups.SkinsShop
{
    public class SkinsShopViewPopup : PopupBase<ISkinsShopPresenter,bool>
    {
        [SerializeField] private Button closePopupButton;

        [SerializeField] private ShopItemListPresenter shopItemList;
        
        private ISkinsShopPresenter presenter;
        
        protected override void Initialize(ISkinsShopPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
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
            closePopupButton.onClick.RemoveListener(OnClosePopup);
            Debug.Log("UnsubscribeUpdates");
        }

        private void OnClosePopup() => 
            SetPopupResult(true);

        [Button]
        private void OnClosePopupClick() => 
            SetPopupResult(true);
        
        public sealed class Factory : AddressablePrefabFactory<SkinsShopViewPopup> { }
    }
}