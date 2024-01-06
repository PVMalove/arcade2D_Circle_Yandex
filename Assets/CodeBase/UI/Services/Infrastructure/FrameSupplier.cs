using System;
using System.Collections.Generic;

namespace CodeBase.UI.Services.Infrastructure
{
    public abstract class FrameSupplier<TKey, TValue> : IFrameSupplier<TKey, TValue> where TValue : UnityFrame
    {
        private readonly Dictionary<TKey, TValue> cashedFrames = new();

        public TValue LoadFrame(TKey key)
        {
            if (cashedFrames.TryGetValue(key, out TValue frame))
            {
                frame.gameObject.SetActive(true);
            }
            else
            {
                frame = InstantiateFrame(key);
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

        protected abstract TValue InstantiateFrame(TKey key);
    }
}