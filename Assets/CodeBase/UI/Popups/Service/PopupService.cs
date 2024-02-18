using System;
using System.Collections.Concurrent;
using System.Threading;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.Service
{
    public sealed class PopupService : IPopupService, IDisposable
    {
        private readonly IFrameSupplierAsync<PopupName, UnityFrame> supplierAsync;
        private readonly ConcurrentDictionary<PopupName, UnityFrame> activePopups = new ConcurrentDictionary<PopupName, UnityFrame>();
        private readonly CancellationTokenSource ctn = new CancellationTokenSource();
        
        public PopupService(IFrameSupplierAsync<PopupName, UnityFrame> supplier)
        {
            supplierAsync = supplier;
        }
        
        public async UniTask ShowPopup<TInitializeData>(PopupName name, TInitializeData initializeData)
        {
            if(IsPopupActive(name)) return;
                
            UnityFrame frame = await supplierAsync.LoadFrame(name);
            activePopups.TryAdd(name, frame);
            
            if (frame is PopupBase<TInitializeData> popupView)
            {
                await popupView.Show(initializeData).AttachExternalCancellation(ctn.Token);
                popupView.Hide();
                activePopups.TryRemove(name, out _);
            }
            else
            {
                throw new InvalidCastException("Received object is not a PopupBase instance");
            }
        }
        
        public bool IsPopupActive(PopupName key) => 
            activePopups.ContainsKey(key);

        public void Dispose()
        {
            ctn.Cancel();
        }
    }
}