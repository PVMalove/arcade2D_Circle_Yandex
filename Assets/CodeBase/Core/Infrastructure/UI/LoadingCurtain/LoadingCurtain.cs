using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Infrastructure.UI.LoadingCurtain
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private CanvasGroup сurtain;

        public void Show()
        {
            Debug.Log("LoadingCurtain -> Show");
            gameObject.SetActive(true);
            сurtain.alpha = 1;
        }
        
        public void Hide()
        {
            FadeIn().Forget();
            Debug.Log("LoadingCurtain -> Hide");
        }

        private async UniTaskVoid FadeIn()
        {
            float fadeStep = 0.05f;
            
            await UniTask.Delay(500);
            while (сurtain.alpha > 0)
            {
                сurtain.alpha -= fadeStep;
                await UniTask.Delay(50);
            }
            
            gameObject.SetActive(false);
        }

        public class Factory : PlaceholderFactory<string, UniTask<LoadingCurtain>>
        {
        }
    }
}