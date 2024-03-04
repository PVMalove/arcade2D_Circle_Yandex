using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Popups.Base
{
    public abstract class PopupBase<TInitializeData> : UnityFrame
    {
        private IPersistentProgressService progressService;
        protected PlayerProgress Progress => progressService.GetProgress();

        private UniTaskCompletionSource taskCompletionSource;

        [Inject]
        public void Construct(IPersistentProgressService progressService) => 
            this.progressService = progressService;

        private void Awake() => 
            OnAwake();

        public UniTask Show(TInitializeData with)
        {
            Debug.Log($"Show + TInitializeData {with}");
            Initialize(with);
            taskCompletionSource = new UniTaskCompletionSource();
            gameObject.SetActive(true);
            return taskCompletionSource.Task;
        }

        public virtual void Hide()
        {
            Debug.Log($"Hide");
            gameObject.SetActive(false);
        }
        
        protected void SetPopupResult() =>
            taskCompletionSource.TrySetResult();

        private void OnEnable()
        {
            SubscribeUpdates();
        }

        private void OnDisable()
        {
            UnsubscribeUpdates();
        }

        protected virtual void OnAwake() { }
        protected virtual void Initialize(TInitializeData with){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnsubscribeUpdates() { }
        protected virtual void Cleanup(){}
    }
}