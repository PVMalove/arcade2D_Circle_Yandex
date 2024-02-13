using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.Service
{
    public sealed class PopupService : IPopupService, IDisposable
    {
        private readonly IFrameSupplierAsync<PopupName, UnityFrame> supplierAsync;
        private readonly Dictionary<PopupName, UnityFrame> activePopups = new Dictionary<PopupName, UnityFrame>();
        private readonly CancellationTokenSource ctn = new CancellationTokenSource();
        
        public PopupService(IFrameSupplierAsync<PopupName, UnityFrame> supplier)
        {
            supplierAsync = supplier;
        }
        public async void ShowPopup<TInitializeData, TResult>(PopupName key, TInitializeData initializeData)
        {
            if(IsPopupActive(key)) return;
            await ShowPopupInternal<TInitializeData, TResult>(key, initializeData);
        }
        
        private async UniTask ShowPopupInternal<TInitializeData, TResult>(PopupName name, TInitializeData initializeData)
        {
            UnityFrame frame = await supplierAsync.LoadFrame(name);
            activePopups.Add(name, frame);
            
            if (frame is PopupBase<TInitializeData, TResult> popupView)
            {
                await popupView.Show(initializeData).AttachExternalCancellation(ctn.Token);
                popupView.Hide();
                activePopups.Remove(name);
            }
            else
            {
                throw new InvalidCastException("Received object is not a PopupBase instance");
            }
        }
        
        public bool IsPopupActive(PopupName key) => 
            activePopups.ContainsKey(key);

        public void Dispose() => 
            ctn.Cancel();
    }
}