using System;
using CodeBase.Core.Data;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;
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
            Initialize(with);
            taskCompletionSource = new UniTaskCompletionSource<TResult>();
            gameObject.SetActive(true);
            return taskCompletionSource.Task;
        }

        public void Hide()
        {
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

        protected virtual void OnAwake() => Hide();
        protected virtual void Initialize(TInitializeData with){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnsubscribeUpdates() { }
        protected virtual void Cleanup(){}
    }
}