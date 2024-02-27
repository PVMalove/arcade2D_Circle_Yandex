using CodeBase.Core.Data;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.Popups.SkinsShop.TEST.Skins.BodySkin;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Popups.Base
{
    public abstract class PopupBase<TInitializeData> : UnityFrame
    {
        private IPersistentProgressStorage progressStorage;
        protected PlayerProgress Progress => progressStorage.Progress;

        private UniTaskCompletionSource taskCompletionSource;

        [Inject]
        public void Construct(IPersistentProgressStorage progressStorage) => 
            this.progressStorage = progressStorage;

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

        protected void addSkin(BodySkins skin)
        {
            Progress.SkinData.OpenBodySkin(skin);
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