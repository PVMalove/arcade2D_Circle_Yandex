using CodeBase.UI.Popups.SkinsShop.TEST.infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Infrastructure
{
    public interface IFrameSupplierAsync_<in TKey, TValue>
    { 
        UniTask<TValue> LoadFrame(TKey key);

        void UnloadFrame(TValue frame);
    }
}