using System.Collections.Generic;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Services.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Popups.Service
{
    public class PopupServiceUnity : MonoBehaviour, IPopupManager<PopupName>
    {
        private IPopupManager<PopupName> manager;
        
        private IFrameSupplierAsync<PopupName, UnityFrame> supplierAsync;
        
        [Inject]
        void Construct(IFrameSupplierAsync<PopupName, UnityFrame>  supplierAsync)
        {
            this.supplierAsync = supplierAsync;
        }

        private void Awake()
        {
            manager = new PopupManager<PopupName, UnityFrame>(supplierAsync);
        }
        
        [Button]
        public void ShowPopup(PopupName key, object args = null)
        {
            manager.ShowPopup(key, args);
        }

        public bool IsPopupActive(PopupName key)
        {
            return manager.IsPopupActive(key);
        }

        [Button]
        public void HidePopup(PopupName key)
        {
            manager.HidePopup(key);
        }

        [Button]
        public void HideAllPopups()
        {
            manager.HideAllPopups();
        }
        
    }

    public class PopupManager<TKey, TValue> : IPopupManager<TKey> where TValue : UnityFrame
    {
        private readonly IFrameSupplierAsync<TKey, TValue> supplier;

        private readonly Dictionary<TKey, TValue> activePopups = new Dictionary<TKey, TValue>();

        private readonly List<TKey> cache = new List<TKey>();

        public PopupManager(IFrameSupplierAsync<TKey, TValue> supplier)
        {
            this.supplier = supplier;
        }
        
        public void ShowPopup(TKey key, object args = default)
        {
            if (!IsPopupActive(key))
            {
                ShowPopupInternal(key, args);
            }
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
        
        private async void ShowPopupInternal(TKey name, object args)
        {
            TValue popup = await supplier.LoadFrame(name);
            popup.OnShow(args);

            activePopups.Add(name, popup);
        }

        private void HidePopupInternal(TKey name)
        {
            TValue popup = activePopups[name];
            popup.OnHide();

            activePopups.Remove(name);
            supplier.UnloadFrame(popup);
        }

        private bool TryFindName(TValue popup, out TKey name)
        {
            foreach (var (key, otherPopup) in this.activePopups)
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

    public interface IPopupManager<in TKey>
    {
        void ShowPopup(TKey key, object args = default);

        bool IsPopupActive(TKey key);

        void HidePopup(TKey key);

        void HideAllPopups();
    }
}