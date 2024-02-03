using System;
using System.Threading;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop;
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

        public PopupService(IFrameSupplierAsync<PopupName, UnityFrame>  supplierAsync,
            IFrameSupplier<PopupName, UnityFrame> supplier,
            SkinsShopPresenter.Factory skinsShopPresenterFactory)
        {
            this.supplierAsync = supplierAsync;
            this.supplier = supplier;
            this.skinsShopPresenterFactory = skinsShopPresenterFactory;
            ctn = new CancellationTokenSource();
        }

        public async UniTask ShowSkinsShop()
        {
            if (await supplierAsync.LoadFrame(PopupName.SKINS_SHOP) is SkinsShopViewPopup skinsShopView)
            {
                ISkinsShopPresenter presenter = skinsShopPresenterFactory.Create();
                await skinsShopView.Show(presenter).AttachExternalCancellation(ctn.Token);
                skinsShopView.Hide();
            }
        }

        public void Dispose() => 
            ctn.Cancel();
    }
}