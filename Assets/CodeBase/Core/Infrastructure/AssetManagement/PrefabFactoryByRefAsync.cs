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

            if (handle.IsDone)
            {
                GameObject newObject = handle.Result;
                return newObject.GetComponent<TComponent>();
            }

            throw new Exception("Asset not loaded");
        }
    }
}