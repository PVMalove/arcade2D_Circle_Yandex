using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.UI.AwaitingOverlay
{
    public class AwaitingOverlayProxy : IAwaitingOverlay
    {
        private readonly AwaitingOverlay.Factory factory;
        private IAwaitingOverlay impl;

        public AwaitingOverlayProxy(AwaitingOverlay.Factory factory) => 
            this.factory = factory;

        public async UniTask InitializeAsync() => 
            impl = await factory.Create(InfrastructureAssetPath.AwaitingOverlayPath);

        public void Show(string withMessage) => impl.Show(withMessage);

        public void Hide() => impl.Hide();
    }
}