using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Zenject;

namespace CodeBase.Core.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly ILogService log;
        private readonly Dictionary<string, AsyncOperationHandle> assetRequests = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> handles = new();
        private readonly Dictionary<string, AsyncOperationHandle> completedCache = new();
        
        [Inject]
        private ConfigLoader configLoader;

        public AssetProvider(ILogService log)
        {
            this.log = log;
        }

        public async UniTask InitializeAsync() => 
            await Addressables.InitializeAsync().ToUniTask();

        public async UniTask<TAsset> Load<TAsset>(string key) where TAsset : class
        {
            if (!assetRequests.TryGetValue(key, out var handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                assetRequests.Add(key, handle);
            }
            
            await handle.ToUniTask();
            
            return handle.Result as TAsset;
        }

        public async UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class
        {
            if (completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completeHandle))
                return (TAsset)completeHandle.Result;

            return await RunWithCacheOnComplete(assetReference.AssetGUID, 
                Addressables.LoadAssetAsync<TAsset>(assetReference));
        }

        public async UniTask<List<string>> GetAssetsListByLabel<TAsset>(string label) => 
            await GetAssetsListByLabel(label, typeof(TAsset));

        public async UniTask<List<string>> GetAssetsListByLabel(string label, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(label, type);

            var locations = await operationHandle.ToUniTask();

            List<string> assetKeys = new List<string>(locations.Count);

            foreach (IResourceLocation location in locations) 
                assetKeys.Add(location.PrimaryKey);
            
            Addressables.Release(operationHandle);
            return assetKeys;
        }

        public async UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class
        {
            List<UniTask<TAsset>> tasks = new List<UniTask<TAsset>>(keys.Count);

            foreach (string key in keys) 
                tasks.Add(Load<TAsset>(key));

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask WarmupAssetsByLabel(string label)
        {
            var assetsList = await GetAssetsListByLabel(label);
            await LoadAll<object>(assetsList);
        }

        public async UniTask ReleaseAssetsByLabel(string label)
        {
            var assetsList = await GetAssetsListByLabel(label);
            
            foreach (string assetKey in assetsList)
                if (assetRequests.TryGetValue(assetKey, out var handler))
                {
                    Addressables.Release(handler);
                    assetRequests.Remove(assetKey);
                }
        }
        
        public UniTask<TConfigObject> GetConfigFromResources<TConfigObject>(string name = "") where TConfigObject : UnityEngine.Object
        {
            TConfigObject config;
    
            List<TConfigObject> scriptableObjects = configLoader.LoadedScriptable
                .OfType<TConfigObject>()
                .ToList();

            if (scriptableObjects.Count > 0)
            {
                if (scriptableObjects.Count > 1)
                    config = scriptableObjects.FirstOrDefault(element => element.name == name);
                else
                    config = scriptableObjects[0];
            }
            else
            {
                Debug.LogWarning("No config objects found from configLoader!");
                return UniTask.FromResult<TConfigObject>(null);
            }

            return UniTask.FromResult(config);
        }

        
        private async UniTask<TAsset> RunWithCacheOnComplete<TAsset>(string cacheKey, AsyncOperationHandle<TAsset> handle) where TAsset : class
        {
            handle.Completed += completeHandle => 
                completedCache[cacheKey] = completeHandle;

            AddHandle(cacheKey, handle);

            await handle.Task;  
            return handle.Result;
        }
        
        private void AddHandle<TAsset>(string key, AsyncOperationHandle<TAsset> handle) where TAsset : class
        {
            if (!handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
        
        public void Cleanup()
        {
            foreach (var assetRequest in assetRequests) 
                Addressables.Release(assetRequest.Value);
            
            assetRequests.Clear();
            log.LogService("Cleanup asset requests", this);
        }
    }
}