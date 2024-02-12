using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.SkinsShop.TEST.infrastructure
{
    public class PopupService_<TKey, TValue> : IPopupService_<TKey, TValue> where TValue: UnityFrame
    {
        private readonly IFrameSupplierAsync<TKey, TValue> supplierAsync;

        private readonly Dictionary<TKey, TValue> activePopups = new Dictionary<TKey, TValue>();

        private readonly List<TKey> cache = new List<TKey>();
        
        private readonly CancellationTokenSource ctn;
        
        public PopupService_(IFrameSupplierAsync<TKey, TValue> supplierAsync)
        {
            this.supplierAsync = supplierAsync;
            ctn = new CancellationTokenSource();
        }
        
        public async UniTask<TValue> ShowPopup(TKey key)
        {
            if (IsPopupActive(key)) return default;
            
            TValue popup = await ShowPopupInternal(key);
            return popup;
        }

        // public async void ShowPopup<T>(TKey key, T initializeData) where T : PopupBase<T,bool>
        // {
        //     TValue popup = await supplierAsync.LoadFrame(key);
        //     
        //     activePopups.Add(key, popup);
        //
        //     if (popup is T View)
        //     {
        //         await View.Show(initializeData).AttachExternalCancellation(ctn.Token);
        //         View.Hide();
        //     }
        //
        // }
        
        public async void ShowPopup<TPopup, TInitializeData>(TKey key, TInitializeData initializeData)
            where TPopup : PopupBase<TInitializeData, bool>, new()
        {
            if (IsPopupActive(key)) return;
                
            TValue loadedObject = await supplierAsync.LoadFrame(key);
            TPopup popup = loadedObject as TPopup ?? throw new InvalidCastException("Не удалось привести загруженный объект к типу TPopup.");
            activePopups.Add(key, loadedObject);

            await popup.Show(initializeData).AttachExternalCancellation(ctn.Token);
            HidePopupInternal(key);
            popup.Hide();
        }

        public bool IsPopupActive(TKey key)
        {
            return activePopups.ContainsKey(key);
        }

        public void HidePopup(TKey key)
        {
            if (IsPopupActive(key))
            {
                HidePopupInternal(key);
            }
        }

        public void HideAllPopups()
        {
            cache.Clear();
            cache.AddRange(activePopups.Keys);

            for (int i = 0, count = cache.Count; i < count; i++)
            {
                var popupName = cache[i];
                HidePopupInternal(popupName);
            }
        }
        
        // private async UniTask<TValue> ShowPopupInternal(TKey name, object args)
        // {
        //     TValue popup = await supplierAsync.LoadFrame(name);
        //     //popup.OnShow(args);
        //     activePopups.Add(name, popup);
        //     return popup;
        // }
        
        private async UniTask<TValue> ShowPopupInternal(TKey key)
        {
            TValue popup = await supplierAsync.LoadFrame(key);
            activePopups.Add(key, popup);
            return popup;
        }

        private void HidePopupInternal(TKey name)
        {
            TValue popup = activePopups[name];
            //popup.OnHide();

            activePopups.Remove(name);
            //supplierAsync.UnloadFrame(popup);
        }

        private bool TryFindName(TValue popup, out TKey name)
        {
            foreach (var (key, otherPopup) in activePopups)
            {
                if (ReferenceEquals(popup, otherPopup))
                {
                    name = key;
                    return true;
                }
            }

            name = default;
            return false;
        }
    }

    public interface IPopupService_<in TKey, TValue>
    {
        UniTask<TValue> ShowPopup(TKey key);
        
        bool IsPopupActive(TKey key);

        void HidePopup(TKey key);

        void HideAllPopups();

        void ShowPopup<TPopup, TInitializeData>(TKey key, TInitializeData initializeData)
            where TPopup : PopupBase<TInitializeData, bool>, new();
    }
}