using System;
using System.Threading;
using CodeBase.Core.GameFlow;
using CodeBase.UI.Root;
using CodeBase.UI.Services.Infrastructure;
using CodeBase.UI.Windows.Base;
using CodeBase.UI.Windows.GameMenu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Windows.Service
{
    public class WindowService : IWindowService, IDisposable
    {
        private readonly IFrameSupplierAsync<WindowName, UnityFrame> supplierAsync;
        private readonly IFrameSupplier<WindowName, UnityFrame> supplier;
        private readonly GameMenuPresenter.Factory gameMenuPresenterFactory;
        private readonly CancellationTokenSource ctn;

        private IUIRoot viewport;

        public WindowService(IFrameSupplierAsync<WindowName, UnityFrame> supplierAsync,
            IFrameSupplier<WindowName, UnityFrame> supplier,
            GameMenuPresenter.Factory gameMenuPresenterFactory)
        {
            this.supplierAsync = supplierAsync;
            this.supplier = supplier;
            this.gameMenuPresenterFactory = gameMenuPresenterFactory;
            ctn = new CancellationTokenSource();
        }

        public async UniTask ShowGameMenu()
        {
            if (await supplierAsync.LoadFrame(WindowName.GAME_MENU) is GameMenuViewWindow gameMenuView)
            {
                IGameMenuPresenter presenter = gameMenuPresenterFactory.Create();
                await gameMenuView.Show(presenter).AttachExternalCancellation(ctn.Token);
                gameMenuView.Hide();
            }
        }

        public void Dispose() =>
            ctn.Cancel();
    }
}