using CodeBase.Core.GameFlow.GameLoading.States;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.GameFlow.GameLoading
{
    public class GameLoadingSceneBootstraper : IInitializable
    {
        private readonly SceneStateMachine sceneStateMachine;
        private readonly StatesFactory statesFactory;
        private readonly ILogService log;

        public GameLoadingSceneBootstraper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory, ILogService log)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.statesFactory = statesFactory;
            this.log = log;
        }

        public void Initialize()
        {
            log.LogState("Start loading scene", this);
            log.LogState($"Build: [{Application.version}]", this);

            sceneStateMachine.RegisterState(statesFactory.Create<LoadPlayerProgressState>());
            sceneStateMachine.RegisterState(statesFactory.Create<FinishGameLoadingState>());

            log.LogState("Finish loading scene", this);
            
            // go to the first scene state
            sceneStateMachine.Enter<LoadPlayerProgressState>().Forget();
        }
    }
}