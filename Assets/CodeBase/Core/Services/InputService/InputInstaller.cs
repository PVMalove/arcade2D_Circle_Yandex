using System;
using UnityEngine;
using YG;
using Zenject;

namespace CodeBase.Core.Services.InputService
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings() =>
            Container.Bind<IInputService>().FromMethod(BindInputService).AsSingle();

        private IInputService BindInputService()
        {
            if (Application.isEditor || YandexGame.EnvironmentData.isDesktop)
            {
                Debug.Log("INPUT STANDALONE");
                return new StandaloneInputService();
            }
            else if (YandexGame.EnvironmentData.isMobile)
            {
                Debug.Log("INPUT MOBILE");
                return new MobileInputService();
            }
            else
            {
                throw new Exception("INPUT - Unknown platform");
            }
        }
    }
}