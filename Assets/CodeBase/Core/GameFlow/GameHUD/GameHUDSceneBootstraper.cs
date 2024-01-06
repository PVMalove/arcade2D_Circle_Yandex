using CodeBase.Core.GameFlow.GameHUD.State;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Core.GameFlow.GameHUD
{
    public class GameHUDSceneBootstraper : IInitializable
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly StatesFactory statesFactory;
        private readonly ILogService log;

        public GameHUDSceneBootstraper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory, 
            ILogService log)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.statesFactory = statesFactory;
            this.log = log;
        }

        public void Initialize()
        {
            log.LogState("Start Initialize GameHUDSceneBootstraper", this);
            sceneStateMachine.RegisterState(statesFactory.Create<MainMenuState>());
            sceneStateMachine.RegisterState(statesFactory.Create<FinishHUDSceneState>());
            log.LogState("Finish Initialize GameHUDSceneBootstraper", this);
            sceneStateMachine.Enter<MainMenuState>().Forget();
        }
    }
}