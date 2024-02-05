using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.Windows.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.GameMenu
{
    public class GameMenuViewScreen : ScreenBase<IGameMenuPresenter, bool>
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button openSkinsShopButton;
        
        private IGameMenuPresenter presenter;
        
        protected override void Initialize(IGameMenuPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            startGameButton.onClick.AddListener(OnStartGame);
            openSkinsShopButton.onClick.AddListener(OnOpenSkinsShop);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            startGameButton.onClick.RemoveListener(OnStartGame);
            openSkinsShopButton.onClick.RemoveListener(OnOpenSkinsShop);
        }

        private void OnStartGame()
        {
            SetWindowResult(true);
            presenter.StartGame();
        }

        private void OnOpenSkinsShop()
        {
            presenter.OpenSkinsShop();
        }

        [Button]
        private void OnClick() => 
            SetWindowResult(true);
        
        public class Factory : AddressablePrefabFactory<GameMenuViewScreen> { }
    }
}