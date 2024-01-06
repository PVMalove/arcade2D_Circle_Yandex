
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.HUD.Base
{
    public abstract class HUDBase<TInitializeData> : UnityFrame
    {
        private void Awake() => 
            OnAwake();

        public void Show(TInitializeData with)
        {
            Initialize(with);
            SubscribeUpdates();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => Hide();
        protected virtual void Initialize(TInitializeData presenter){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
}