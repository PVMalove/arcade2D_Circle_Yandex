using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CodeBase.Core.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private ILogService log;

        public SceneLoader(ILogService log) => 
            this.log = log;

        public async UniTask Load(string nextScene)
        {
            AsyncOperationHandle<SceneInstance> handler = Addressables.LoadSceneAsync(nextScene, LoadSceneMode.Single, false);

            await handler.ToUniTask();
            await handler.Result.ActivateAsync().ToUniTask();
        }
    }
}