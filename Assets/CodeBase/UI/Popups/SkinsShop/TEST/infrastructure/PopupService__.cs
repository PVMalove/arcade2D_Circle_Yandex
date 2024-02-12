// using System.Collections.Generic;
// using System.Threading;
// using CodeBase.UI.Popups.Base;
// using CodeBase.UI.Services.Infrastructure;
// using Cysharp.Threading.Tasks;
//
// namespace CodeBase.UI.Popups.SkinsShop.TEST.infrastructure
// {
//     public class PopupService__<TValue, TInitializeData> : IPopupService__<TInitializeData>
//         where TValue : UnityFrame_<TInitializeData>
//     {
//         private readonly IFrameSupplierAsync_<PopupName, TValue> supplierAsync;
//
//         private readonly Dictionary<PopupName, TValue> activePopups = new Dictionary<PopupName, TValue>();
//
//         private readonly List<PopupName> cache = new List<PopupName>();
//
//         private readonly CancellationTokenSource ctn;
//         
//         private UniTaskCompletionSource<TResult> taskCompletionSource;
//         
//         public PopupService__(IFrameSupplierAsync_<PopupName, TValue> supplierAsync)
//         {
//             this.supplierAsync = supplierAsync;
//             ctn = new CancellationTokenSource();
//         }
//         
//         public UniTask<TResult> ShowPopup(PopupName key, TInitializeData args = default)
//         {
//             if (!IsPopupActive(key))
//             {
//                 ShowPopupInternal(key, args);
//                 return taskCompletionSource.Task;
//             }
//             return taskCompletionSource.Task;
//         }
//
//         public bool IsPopupActive(PopupName key)
//         {
//             return activePopups.ContainsKey(key);
//         }
//
//         public void HidePopup(PopupName key)
//         {
//             if (IsPopupActive(key))
//             {
//                 HidePopupInternal(key);
//             }
//         }
//
//         public void HideAllPopups()
//         {
//             cache.Clear();
//             cache.AddRange(activePopups.Keys);
//
//             for (int i = 0, count = cache.Count; i < count; i++)
//             {
//                 var popupName = cache[i];
//                 HidePopupInternal(popupName);
//             }
//         }
//         
//         private async void ShowPopupInternal(PopupName name, TInitializeData args)
//         {
//             TValue popup = await supplierAsync.LoadFrame(name);
//             activePopups.Add(name, popup);
//             popup.OnShow(args).AttachExternalCancellation(ctn.Token);
//             HidePopup(name);
//         }
//
//         private void HidePopupInternal(PopupName name)
//         {
//             TValue popup = activePopups[name];
//             popup.OnHide();
//
//             activePopups.Remove(name);
//             supplierAsync.UnloadFrame(popup);
//         }
//
//         private bool TryFindName(TValue popup, out PopupName name)
//         {
//             foreach (var (key, otherPopup) in activePopups)
//             {
//                 if (ReferenceEquals(popup, otherPopup))
//                 {
//                     name = key;
//                     return true;
//                 }
//             }
//
//             name = default;
//             return false;
//         }
//     }
//     public interface IPopupService__<in TInitializeData, TResult>
//     {
//         UniTask<TResult> ShowPopup(PopupName key, TInitializeData args = default);
//
//         bool IsPopupActive(PopupName key);
//
//         void HidePopup(PopupName key);
//
//         void HideAllPopups();
//     }
// }