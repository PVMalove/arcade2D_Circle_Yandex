using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.Core.Infrastructure.AssetManagement
{
    public class AddressablePrefabFactory<TComponent> where TComponent : MonoBehaviour
    {
        private  IInstantiator instantiator;
        private  IAssetProvider assetProvider;

        [Inject]
        public void Construct(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            this.instantiator = instantiator;
            this.assetProvider = assetProvider;
        }
 
        public async UniTask<TComponent> Create(string assetKey)
        {
            GameObject prefab = await GetPrefab(assetKey);
            return InstantiatePrefab(prefab);
        }
 
        public async UniTask<TComponent> Create(string assetKey, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject prefab = await GetPrefab(assetKey);
            return InstantiatePrefabAs(prefab, position, rotation, parent);
        }
 
        public async UniTask<TComponent> Create(AssetReferenceGameObject assetRef)
        {
            GameObject prefab = await GetPrefab(assetRef);
            return InstantiatePrefab(prefab);
        }
 
        public async UniTask<TComponent> Create(AssetReferenceGameObject assetRef, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject prefab = await GetPrefab(assetRef);
            return InstantiatePrefabAs(prefab, position, rotation, parent);
        }
       
        private TComponent InstantiatePrefab(GameObject prefab)
        {
            GameObject newObject = instantiator.InstantiatePrefab(prefab);
            TComponent component = newObject.GetComponent<TComponent>();
            return component;
        }

        private TComponent InstantiatePrefabAs(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject newObject = instantiator.InstantiatePrefab(prefab, position, rotation, parent);
            TComponent component = newObject.GetComponent<TComponent>();
            return component;
        }

        private async UniTask<GameObject> GetPrefab(string assetKey) => 
            await assetProvider.Load<GameObject>(assetKey);

        private async UniTask<GameObject> GetPrefab(AssetReferenceGameObject assetRef) => 
            await assetProvider.Load<GameObject>(assetRef);
    }
}
