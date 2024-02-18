using System.Collections.Generic;
using CodeBase.Core.GameFlow.GameMode.GameWorld;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.AwaitingOverlay;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.LogService;
using CodeBase.Gameplay.Environment;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class StartGameModeState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IAwaitingOverlay awaitingOverlay;
        private readonly IInitializeGameWorld gameWorld;
        private readonly SceneStateMachine sceneStateMachine;
        private readonly ILogService log;
        private readonly Burger.Factory spawnerFactory;

        public StartGameModeState(ILoadingCurtain loadingCurtain,
            IAwaitingOverlay awaitingOverlay,
            IInitializeGameWorld gameWorld, 
            SceneStateMachine sceneStateMachine, ILogService log, Burger.Factory spawnerFactory)
        {
            this.gameWorld = gameWorld;
            this.sceneStateMachine = sceneStateMachine;
            this.log = log;
            this.spawnerFactory = spawnerFactory;
            this.loadingCurtain = loadingCurtain;
            this.awaitingOverlay = awaitingOverlay;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            //await SPAWN();
            gameWorld.InitGameWorld();
            loadingCurtain.Hide();
            awaitingOverlay.Hide();
            await sceneStateMachine.Enter<PlayGameModeState>();
        }
        private async UniTask SPAWN()
        {
            List<UniTask> tasks = new List<UniTask>
            {
                SpawnBurgers(),
            };

            await UniTask.WhenAll(tasks);
            log.LogService("SPAWN", this);
        }

        private async UniTask SpawnBurgers()
        {
            int numberOfCubes = 5000;
            float spawnRadius = 40f;
            List<GameObject> cache = new List<GameObject>();
            
            for (int i = 0; i < numberOfCubes; i++)
            {
                GameObject burger = spawnerFactory.Create().gameObject;

                Vector3 randomPosition = burger.transform.position + Random.insideUnitSphere * spawnRadius;
                burger.transform.position = randomPosition;
                burger.name = "Burger_" + i;

                cache.Add(burger);
                
            }
            foreach (var burger in cache)
            {
                burger.transform.rotation = Random.rotation;
            }
            cache.Clear();
            await UniTask.Delay(5000);
        }

        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return default;
        }
    }
}