using System;
using System.Threading;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Root;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Popups.Service
{
    public class PopupService : IPopupService, IDisposable
    {
        private readonly IUIFactory uiFactory;
        private readonly IFrameSupplierAsync<PopupName, UnityFrame> supplierAsync;
        private readonly CancellationTokenSource ctn;

        private IUIRoot viewport;

        public PopupService(IUIFactory uiFactory,
            IFrameSupplierAsync<PopupName, UnityFrame>  supplierAsync)
        {
            this.uiFactory = uiFactory;
            this.supplierAsync = supplierAsync;
            ctn = new CancellationTokenSource();
        }

        public void Dispose() => 
            ctn.Cancel();
    }
}