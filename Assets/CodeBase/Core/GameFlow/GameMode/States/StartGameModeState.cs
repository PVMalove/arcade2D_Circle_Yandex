using CodeBase.Core.GameFlow.GameMode.GameWorld;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class StartGameModeState : IState
    {
        private readonly IInitializeGameWorld gameWorld;
        private readonly SceneStateMachine sceneStateMachine;
        public StartGameModeState(IInitializeGameWorld gameWorld, SceneStateMachine sceneStateMachine)
        {
            this.gameWorld = gameWorld;
            this.sceneStateMachine = sceneStateMachine;
        }

        public async UniTask Enter()
        {
            gameWorld.InitGameWorld();
            // you can use states like this for showing starting cut scenes, objectives on the level, explaining game rules and so on
            await sceneStateMachine.Enter<PlayGameModeState>();
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}