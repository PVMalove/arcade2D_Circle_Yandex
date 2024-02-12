// using Cysharp.Threading.Tasks;
//
// namespace CodeBase.UI.Popups.SkinsShop.TEST.infrastructure
// {
//     public abstract class PopupBase_<TInitializeData, TResult> : UnityFrame_<TInitializeData, TResult>
//     {
//         private UniTaskCompletionSource<TResult> taskCompletionSource;
//         
//         public override UniTask<TResult> OnShow(TInitializeData with)
//         {
//             Initialize(with);
//             taskCompletionSource = new UniTaskCompletionSource<TResult>();
//             gameObject.SetActive(true);
//             return taskCompletionSource.Task;
//         }
//
//         public override void OnHide()
//         {
//             gameObject.SetActive(false);
//         }
//         protected void SetPopupResult(TResult result) =>
//             taskCompletionSource.TrySetResult(result);
//         
//         protected virtual void OnAwake() => OnHide();
//         protected virtual void Initialize(TInitializeData with){}
//         protected virtual void SubscribeUpdates(){}
//         protected virtual void UnsubscribeUpdates() { }
//         protected virtual void Cleanup(){}
//     }
// }