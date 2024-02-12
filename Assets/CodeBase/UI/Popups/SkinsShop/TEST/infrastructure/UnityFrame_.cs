// using CodeBase.UI.Popups.Base;
// using CodeBase.UI.Services.Infrastructure;
// using Cysharp.Threading.Tasks;
// using UnityEngine;
//
// namespace CodeBase.UI.Popups.SkinsShop.TEST.infrastructure
// {
//     public abstract class UnityFrame_<TInitializeData, TResult> : MonoBehaviour
//     {
//         public abstract UniTask<TResult> OnShow(TInitializeData with);
//         public abstract void OnHide();
//     }
//     
//     public class PopupService_2<TKey, TValue, TInitializeData, TResult> : IPopupService_<TKey> where TValue : UnityFrame_<TInitializeData, TResult>
//     {
//         private readonly IFrameSupplierAsync_<TKey, TValue> supplierAsync;
//
//         public PopupService_2(IFrameSupplierAsync_<TKey, TValue> supplierAsync)
//         {
//             this.supplierAsync = supplierAsync;
//         }
//
//         public async void ShowPopup(TKey key, object args = default)
//         {
//             TValue popup = await supplierAsync.LoadFrame(key);
//             if (args is TInitializeData initData)
//             {
//                 popup.OnShow(initData).Forget();
//             }
//         }
//
//         public bool IsPopupActive(TKey key)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void HidePopup(TKey key)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void HideAllPopups()
//         {
//             throw new System.NotImplementedException();
//         }
//     }
// }