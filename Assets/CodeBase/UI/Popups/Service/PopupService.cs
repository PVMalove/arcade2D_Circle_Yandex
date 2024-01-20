using System;
using System.Threading;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Root;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;
using CodeBase.UI.Windows.GameMenu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.Service
{
    public class PopupService : IPopupService, IDisposable
    {
        private readonly IUIFactory uiFactory;
        private readonly IFrameSupplierAsync<PopupName, UnityFrame> supplierAsync;
        private readonly IFrameSupplier<PopupName, UnityFrame> supplier;
        private readonly GameMenuPresenter.Factory gameMenuPresenterFactory;
        private readonly CancellationTokenSource ctn;

        private IUIRoot viewport;

        public PopupService(IUIFactory uiFactory,
            IFrameSupplierAsync<PopupName, UnityFrame>  supplierAsync,
            IFrameSupplier<PopupName, UnityFrame> supplier,
            GameMenuPresenter.Factory gameMenuPresenterFactory)
        {
            this.uiFactory = uiFactory;
            this.supplierAsync = supplierAsync;
            this.supplier = supplier;
            this.gameMenuPresenterFactory = gameMenuPresenterFactory;
            ctn = new CancellationTokenSource();
        }

        public void Dispose() => 
            ctn.Cancel();
    }
}