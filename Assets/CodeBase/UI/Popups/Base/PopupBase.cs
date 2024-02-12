using CodeBase.Core.Data;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Popups.Base
{
    public abstract class PopupBase<TInitializeData, TResult> : UnityFrame
    {
        private IPersistentProgressStorage progressStorage;
        protected PlayerProgress Progress => progressStorage.Progress;

        private UniTaskCompletionSource<TResult> taskCompletionSource;

        [Inject]
        public void Construct(IPersistentProgressStorage progressStorage) => 
            this.progressStorage = progressStorage;

        private void Awake() => 
            OnAwake();

        public UniTask<TResult> Show(TInitializeData with)
        {
            Debug.Log($"Show + TInitializeData {with}");
            Initialize(with);
            taskCompletionSource = new UniTaskCompletionSource<TResult>();
            gameObject.SetActive(true);
            return taskCompletionSource.Task;
        }

        public virtual void Hide()
        {
            Debug.Log($"Hide");
            gameObject.SetActive(false);
        }
        
        protected void SetPopupResult(TResult result) =>
            taskCompletionSource.TrySetResult(result);

        private void OnEnable()
        {
            SubscribeUpdates();
        }

        private void OnDisable()
        {
            UnsubscribeUpdates();
        }
        
        public override void OnShow(object with)
        {
            Debug.Log($"OnShow + Args {with}");
        }

        public override void OnHide()
        {
            Debug.Log($"OnHide");
        }

        protected virtual void OnAwake() => Hide();
        protected virtual void Initialize(TInitializeData with){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnsubscribeUpdates() { }
        protected virtual void Cleanup(){}
    }
}