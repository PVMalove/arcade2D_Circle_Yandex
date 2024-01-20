using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.UI.Windows.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.GameMenu
{
    public class GameMenuViewWindow : WindowBase<IGameMenuPresenter, bool>
    {
        [SerializeField] private Button startGameButton;
        
        private IGameMenuPresenter presenter;
        
        protected override void Initialize(IGameMenuPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            startGameButton.onClick.AddListener(StartGame);
        }
        
        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            startGameButton.onClick.RemoveListener(StartGame);
        }
        
        private void StartGame()
        {
            SetWindowResult(true);
            presenter.StartGame();
        }
        
        [Button]
        private void OnClick() => 
            SetWindowResult(true);
        
        public class Factory : AddressablePrefabFactory<GameMenuViewWindow> { }
    }
}