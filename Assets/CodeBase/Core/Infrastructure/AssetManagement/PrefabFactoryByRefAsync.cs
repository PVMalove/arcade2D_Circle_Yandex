using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace CodeBase.Core.Infrastructure.AssetManagement
{
    public class PrefabFactoryByRefAsync<TComponent> : IFactory<AssetReferenceGameObject, UniTask<TComponent>>
    {
        public async UniTask<TComponent> Create(AssetReferenceGameObject assetReference)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(assetReference);
            await handle;

            if (!handle.IsDone) throw new Exception("Asset not loaded");
            
            GameObject newObject = handle.Result;
            return newObject.GetComponent<TComponent>();
        }
    }
}