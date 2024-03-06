using CodeBase.Core.GameFlow.GameMode.States;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Gameplay.Environment;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.Service;
using CodeBase.UI.Popups.Shop;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.GameMenu
{
    public sealed class GameMenuPresenter : IGameMenuPresenter
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly IGameFactory gameFactory;
        private readonly IPopupService popupService;
        private readonly SkinsShopPresenter.Factory skinsShopPresenterFactory;
        public GameMenuPresenter(SceneStateMachine sceneStateMachine, IGameFactory gameFactory,
            IPopupService popupService, SkinsShopPresenter.Factory skinsShopPresenterFactory)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.gameFactory = gameFactory;
            this.popupService = popupService;
            this.skinsShopPresenterFactory = skinsShopPresenterFactory;
        }

        public async void StartGame()
        {
            await sceneStateMachine.Enter<PlayGameModeState>();
            gameFactory.CircleBackground.GetComponent<CircleBackgroundAnimation>().StartGameAnimation();
            //todo DELETE, FOR TEST
            await UniTask.Delay(1000);
            GameObject load = Resources.Load<GameObject>("Gameplay/Obstacles");
            GameObject.Instantiate(load);
        }

        public void OpenSkinsShop()
        {
            ISkinsShopPresenter presenter = skinsShopPresenterFactory.Create();
            popupService.ShowPopup(PopupName.SKINS_SHOP, presenter);
        }

        public sealed class Factory : PlaceholderFactory<IGameMenuPresenter>
        {
        }
    }
}