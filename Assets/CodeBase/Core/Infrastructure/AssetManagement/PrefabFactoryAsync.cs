using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Infrastructure.AssetManagement
{
    public class PrefabFactoryAsync<TComponent> : IFactory<string, UniTask<TComponent>>
    {
        private readonly IInstantiator instantiator;
        private readonly IAssetProvider assetProvider;

        public PrefabFactoryAsync(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            this.instantiator = instantiator;
            this.assetProvider = assetProvider;
        }

        public async UniTask<TComponent> Create(string assetKey)
        {
            GameObject prefab = await assetProvider.Load<GameObject>(assetKey);
            GameObject newObject = instantiator.InstantiatePrefab(prefab);
            // newObject.name = assetKey;
            return newObject.GetComponent<TComponent>();
        }
    }
}