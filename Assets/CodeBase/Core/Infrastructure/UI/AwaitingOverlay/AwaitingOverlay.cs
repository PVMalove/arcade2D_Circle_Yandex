using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Infrastructure.UI.AwaitingOverlay
{
    public class AwaitingOverlay : MonoBehaviour, IAwaitingOverlay
    {
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private Canvas canvas;

        private void Awake() => 
            Hide();

        public void Show(string withMessage)
        {
            canvas.enabled = true;
        }

        public void Hide() => canvas.enabled = false;

        public class Factory : PlaceholderFactory<string, UniTask<AwaitingOverlay>>
        {
        }
    }
}