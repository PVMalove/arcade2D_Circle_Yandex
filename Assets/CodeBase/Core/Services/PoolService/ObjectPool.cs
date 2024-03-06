using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Core.Services.PoolService
{
    public class ObjectPool<TComponent> where TComponent : MonoBehaviour
    {
        private readonly IPoolFactory factory;
        
        public PoolObjectType Type { get; private set; }

        private AssetReferenceGameObject objectReference;
        private Transform parent;
        private Stack<TComponent> entries;
        
        public ObjectPool(IPoolFactory factory)
        {
            this.factory = factory;
        }
        
        public async UniTask InitializeAsync(AssetReferenceGameObject objectReference, int startCapacity, 
            PoolObjectType type, Transform parent)
        {
            this.objectReference = objectReference;
            Type = type;
            this.parent = parent;

            entries = new Stack<TComponent>(startCapacity);
        
            List<UniTask> tasks = new List<UniTask>(startCapacity);
            for (int i = 0; i < startCapacity; i++)
            {
                tasks.Add(AddObject());
            }
        
            await UniTask.WhenAll(tasks);
        }
        
        public async UniTask<TComponent> Get(Vector3 position, Transform parent = null)
        {
            if (entries.Count == 0)
            {
                await AddObject();
            }
        
            TComponent poolObject = entries.Pop();
            
            poolObject.transform.position = position;
            if (parent != null)
            {
                poolObject.transform.SetParent(parent);
            }
            poolObject.gameObject.SetActive(true);
            
            return poolObject;
        }
        
        public void Return(TComponent poolObject)
        {
            poolObject.gameObject.SetActive(false);
            poolObject.transform.position = parent.transform.position;
            poolObject.transform.SetParent(parent);
            
            entries.Push(poolObject);
        }
        
        private async UniTask AddObject()
        {
            TComponent newObject = await factory.CreateAsync<TComponent>(objectReference, parent.transform.position, parent);
            newObject.gameObject.SetActive(false);
            entries.Push(newObject);
        }
    }
}