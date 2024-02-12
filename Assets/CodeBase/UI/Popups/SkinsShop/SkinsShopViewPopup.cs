using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop.TEST;
using CodeBase.UI.Popups.SkinsShop.TEST.infrastructure;
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
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            closePopupButton.onClick.AddListener(OnClosePopup);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            closePopupButton.onClick.RemoveListener(OnClosePopup);
        }

        private void OnClosePopup() => 
            SetPopupResult(true);

        [Button]
        private void OnClosePopupClick() => 
            SetPopupResult(true);
        
        public sealed class Factory : AddressablePrefabFactory<SkinsShopViewPopup> { }
    }
}