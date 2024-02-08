using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Infrastructure
{
    public abstract class FrameSupplierAsync<TKey, TValue> : IFrameSupplierAsync<TKey, TValue> where TValue : UnityFrame
   {
        private readonly Dictionary<TKey, TValue> cashedFrames = new();

        public async UniTask<TValue> LoadFrame(TKey key)
        {
            if (cashedFrames.TryGetValue(key, out TValue frame))
            {
                frame.gameObject.SetActive(true);
            }
            else
            {
                frame = await InstantiateFrame(key) as TValue;
                cashedFrames.Add(key, frame);
            }

            if (frame == null) 
                throw new InvalidOperationException($"Invalid key: {key}");;
            
            frame.transform.SetAsLastSibling();
            return frame;
        }

        public void UnloadFrame(TValue frame)
        {
            frame.gameObject.SetActive(false);
            if(TryFindName(frame, out var name))
            {
                cashedFrames.Remove(name);                 
            }
        }
        
        private bool TryFindName(TValue frame, out TKey name)
        {
            foreach (var (key, otherFrame) in cashedFrames)
            {
                if (!ReferenceEquals(frame, otherFrame)) continue;
                name = key;
                return true;
            }

            name = default;
            return false;
        }

        protected abstract UniTask<UnityFrame> InstantiateFrame(TKey key);
    }
}