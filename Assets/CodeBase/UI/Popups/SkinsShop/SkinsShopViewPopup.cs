using System;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop.TEST;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins.BodySkin;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popups.SkinsShop
{
    public class SkinsShopViewPopup : PopupBase<ISkinsShopPresenter>
    {
        [SerializeField] private Button closePopupButton;

        //[SerializeField] private ShopItemListPresenter shopItemList;
        [SerializeField] private ShopItemsPresenter shopItemList;
        
        private ISkinsShopPresenter presenter;

        private async void Start()
        {
            await shopItemList.InitializeAsync();
            Debug.Log("1");
        }

        protected async override void Initialize(ISkinsShopPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            this.presenter.InitializeShop();
            Debug.Log("2");
            //await shopItemList.SetItems(presenter.ShopItemsCatalog.SkinItems);
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
            SetPopupResult();

        // [Button]
        // private void open()=>
        //     addSkin(BodySkins.green_body_circle);
        
        [Button]
        private void OnClosePopupClick() => 
            SetPopupResult();
        
        public sealed class Factory : AddressablePrefabFactory<SkinsShopViewPopup> { }
    }
}