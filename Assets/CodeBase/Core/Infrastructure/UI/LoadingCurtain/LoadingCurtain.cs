using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Infrastructure.UI.LoadingCurtain
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        public CanvasGroup Curtain;

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }
        
        public void Hide() => 
            FadeIn().Forget();

        private async UniTaskVoid FadeIn()
        {
            float fadeStep = 0.05f;
            
            await UniTask.Delay(500);
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= fadeStep;
                await UniTask.Delay(50);
            }
            
            gameObject.SetActive(false);
        }

        public class Factory : PlaceholderFactory<string, UniTask<LoadingCurtain>>
        {
        }
    }
}