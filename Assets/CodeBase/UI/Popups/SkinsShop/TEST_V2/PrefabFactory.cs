﻿using CodeBase.Core.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly IInstantiator instantiator;
        private readonly IAssetProvider assetProvider;

        public PrefabFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            this.instantiator = instantiator;
            this.assetProvider = assetProvider;
        }
        
        public async UniTask<TComponent> CreateAsync<TComponent>(string key, Vector3 position, Transform parent = null) 
            where TComponent : MonoBehaviour
        {
            var prefab = await assetProvider.Load<GameObject>(key);
            return instantiator.InstantiatePrefabForComponent<TComponent>(prefab, position, Quaternion.identity, parent);
        }

        public async UniTask<TComponent> CreateAsync<TComponent>(AssetReference reference, Vector3 position, 
            Transform parent = null) where TComponent : MonoBehaviour
        {
            position = Vector3.zero;
            var prefab = await assetProvider.Load<GameObject>(reference);
            return instantiator.InstantiatePrefabForComponent<TComponent>(prefab, position, Quaternion.identity, parent);
        }
    }
}