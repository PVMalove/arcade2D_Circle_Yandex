using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Infrastructure
{
    public interface IFrameSupplierAsync<in TKey, TValue> where TValue : UnityFrame
    { 
        UniTask<TValue> LoadFrame(TKey key);

        void UnloadFrame(TValue frame);
    }
}