using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Infrastructure.UI.AwaitingOverlay
{
    public class AwaitingOverlay : MonoBehaviour, IAwaitingOverlay
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup overlay;

        public void Show()
        {
            FadeOut().Forget();
            overlay.alpha = 1;
            Debug.Log("AwaitingOverlay -> Show");
        }

        public void Hide()
        {
            FadeIn().Forget();
            Debug.Log("AwaitingOverlay -> Hide");
        }

        private async UniTaskVoid FadeIn()
        {
            float fadeStep = 0.05f;
            
            await UniTask.Delay(500);
            while (overlay.alpha > 0)
            {
                overlay.alpha -= fadeStep;
                await UniTask.Delay(50);
            }
            
            canvas.enabled = false;
        }
        
        private async UniTaskVoid FadeOut()
        {
            float fadeStep = 0.05f;
            
            await UniTask.Delay(500);
            while (overlay.alpha > 1)
            {
                overlay.alpha += fadeStep;
                await UniTask.Delay(50);
            }
            
            canvas.enabled = true;
        }
        public class Factory : PlaceholderFactory<string, UniTask<AwaitingOverlay>>
        {
        }
    }
}