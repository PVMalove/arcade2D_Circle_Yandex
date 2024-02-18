using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.UI.AwaitingOverlay
{
    public class AwaitingOverlayProxy : IAwaitingOverlay
    {
        private AwaitingOverlay.Factory factory;
        private IAwaitingOverlay impl;

        public AwaitingOverlayProxy(AwaitingOverlay.Factory factory) => 
            this.factory = factory;

        public async UniTask InitializeAsync() => 
            impl = await factory.Create(InfrastructureAssetPath.AwaitingOverlay);

        public void Show() => 
            impl.Show();

        public void Hide() => 
            impl.Hide();
    }
}