using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.Shop.Item;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popups.Shop
{
    public class SkinsShopViewPopup : PopupBase<ISkinsShopPresenter>
    {
        [SerializeField] private TextMeshProUGUI coinsAmountText;
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
            
            presenter.InitializeShopItems();
            await shopItemList.SetSkinItems(presenter.SkinItems);
            
            presenter.Subscribe();
            presenter.ChangedCoinsAmount += OnCoinsAmountChanged;
            OnCoinsAmountChanged();
            
            Debug.Log("Initialize");
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            closePopupButton.onClick.AddListener(OnClosePopup);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            presenter.Unsubscribe();
            shopItemList.Cleanup();
            closePopupButton.onClick.RemoveListener(OnClosePopup);
        }

        private void OnClosePopup() => 
            SetPopupResult();
        
        private void OnCoinsAmountChanged() => 
            coinsAmountText.text = presenter.CoinsAmount;
        
        [Button]
        private void OnClosePopupClick() => 
            SetPopupResult();
        
        public sealed class Factory : AddressablePrefabFactory<SkinsShopViewPopup> { }
    }
}