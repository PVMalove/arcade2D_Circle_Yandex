using CodeBase.Core.GameFlow.GameMode.States;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Gameplay.Environment;
using Zenject;

namespace CodeBase.UI.Windows.GameMenu
{
    public class GameMenuPresenter : IGameMenuPresenter
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly IGameFactory gameFactory;

        public GameMenuPresenter(SceneStateMachine sceneStateMachine, IGameFactory gameFactory)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.gameFactory = gameFactory;
        }

        public async void StartGame()
        {
            await sceneStateMachine.Enter<PlayGameModeState>();
            gameFactory.CircleBackground.GetComponent<CircleBackgroundAnimation>().StartGameAnimation();
        }
        
        public sealed class Factory : PlaceholderFactory<IGameMenuPresenter>
        {
        }
    }
}