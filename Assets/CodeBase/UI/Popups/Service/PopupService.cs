using System;
using System.Threading;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop;
using CodeBase.UI.Popups.SkinsShop.TEST.infrastructure;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.Service
{
    public class PopupService : IPopupService, IDisposable
    {
        private readonly IFrameSupplierAsync<PopupName, UnityFrame> supplierAsync;
        private readonly IFrameSupplier<PopupName, UnityFrame> supplier;
        private readonly SkinsShopPresenter.Factory skinsShopPresenterFactory;
        private readonly CancellationTokenSource ctn;
        private readonly IPopupService_<PopupName, UnityFrame> service;

        public PopupService(IFrameSupplierAsync<PopupName, UnityFrame>  supplierAsync,
            IFrameSupplier<PopupName, UnityFrame> supplier,
            SkinsShopPresenter.Factory skinsShopPresenterFactory,
            IPopupService_<PopupName, UnityFrame> service)
        {
            this.supplierAsync = supplierAsync;
            this.supplier = supplier;
            this.skinsShopPresenterFactory = skinsShopPresenterFactory;
            this.service = service;
            ctn = new CancellationTokenSource();
        }

        public async UniTask ShowSkinsShop()
        {
            ISkinsShopPresenter presenter = skinsShopPresenterFactory.Create();

            service.ShowPopup<SkinsShopViewPopup, ISkinsShopPresenter>(PopupName.SKINS_SHOP, presenter);

            // SkinsShopViewPopup showPopup = await service.ShowPopup(PopupName.SKINS_SHOP) as SkinsShopViewPopup;
            // if (showPopup != null)
            // {
            //     await showPopup.Show(presenter).AttachExternalCancellation(ctn.Token);
            //     showPopup.Hide();
            // }

            // if (await supplierAsync.LoadFrame(PopupName.SKINS_SHOP) is SkinsShopViewPopup skinsShopView)
            // {
            //     ISkinsShopPresenter presenter = skinsShopPresenterFactory.Create();
            //     await skinsShopView.Show(presenter).AttachExternalCancellation(ctn.Token);
            //     skinsShopView.Hide();
            // }
        }

        public void Dispose() => 
            ctn.Cancel();
    }
}