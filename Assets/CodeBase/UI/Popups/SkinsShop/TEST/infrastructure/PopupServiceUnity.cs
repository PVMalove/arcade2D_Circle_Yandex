// using System;
// using CodeBase.UI.Popups.Base;
// using CodeBase.UI.Services.Infrastructure;
// using Cysharp.Threading.Tasks;
// using JetBrains.Annotations;
// using Sirenix.OdinInspector;
// using UnityEngine;
// using Zenject;
//
// namespace CodeBase.UI.Popups.SkinsShop.TEST.infrastructure
// {
//     public class PopupServiceUnity : MonoBehaviour, IPopupService_<PopupName, UnityFrame>
//     {
//         // private IPopupService_<PopupName> popupService;
//         //
//         // [Inject]
//         // void Construct(IPopupService_<PopupName> manager)
//         // {
//         //     this.popupService = manager;
//         // }
//         //
//         // [Button]
//         // public UniTask<UnityFrame> ShowPopup(PopupName key, object args = null)
//         // {
//         //     popupService.ShowPopup(key, args);
//         //     return default;
//         // }
//         //
//         // public bool IsPopupActive(PopupName key)
//         // {
//         //     return popupService.IsPopupActive(key);
//         // }
//         //
//         // [Button]
//         // public void HidePopup(PopupName key)
//         // {
//         //     popupService.HidePopup(key);
//         // }
//         //
//         // [Button]
//         // public void HideAllPopups()
//         // {
//         //     popupService.HideAllPopups();
//         // }
//
//
//         public UniTask<TObject> ShowPopup<TObject>(PopupName key) where TObject : UnityFrame
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public UniTask<UnityFrame> ShowPopup(PopupName key)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public bool IsPopupActive(PopupName key)
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public void HidePopup(PopupName key)
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