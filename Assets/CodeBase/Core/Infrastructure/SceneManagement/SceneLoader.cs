using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Core.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string nextScene)
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            AsyncOperationHandle<SceneInstance> handler =
                Addressables.LoadSceneAsync(nextScene, LoadSceneMode.Single, false);
            
            handler.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"Scene {nextScene} loaded successfully.");
                    handler.Result.ActivateAsync().ToUniTask();
                }
                else
                {
                    Debug.LogError($"Failed to load scene {nextScene}.");
                }
            };
            
            await handler.ToUniTask();
        }
    }
}