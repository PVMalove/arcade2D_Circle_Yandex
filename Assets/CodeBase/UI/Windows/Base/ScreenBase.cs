using CodeBase.Core.Data;
using CodeBase.Core.Services.ProgressService;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.UI.Windows.Base
{
    public class ScreenBase<TInitializeData, TResult> : UnityFrame
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
            taskCompletionSource = new UniTaskCompletionSource<TResult>();
            Initialize(with);
            gameObject.SetActive(true);
            return taskCompletionSource.Task;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        protected void SetScreenResult(TResult result) =>
            taskCompletionSource.TrySetResult(result);

        private void OnEnable()
        {
            SubscribeUpdates();
        }
        private void OnDisable()
        {
            UnsubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => Hide();
        protected virtual void Initialize(TInitializeData with){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnsubscribeUpdates() { }
        protected virtual void Cleanup(){}
    }
}