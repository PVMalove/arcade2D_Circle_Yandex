using CodeBase.Core.Data;
using CodeBase.Core.Services.PlayerProgressService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button CloseButton;
    
        protected IPersistentProgressStorage ProgressStorage;
        protected PlayerProgress Progress => ProgressStorage.Progress;

        [Inject]
        public void Construct(IPersistentProgressStorage progressStorage) => 
            ProgressStorage = progressStorage;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => 
            CloseButton?.onClick.AddListener(()=> Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
}
