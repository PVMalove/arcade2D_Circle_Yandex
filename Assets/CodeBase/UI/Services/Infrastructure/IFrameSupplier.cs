namespace CodeBase.UI.Services.Infrastructure
{
    public interface IFrameSupplier<in TKey, TValue> where TValue : UnityFrame
    { 
        TValue LoadFrame(TKey key);

        void UnloadFrame(TValue frame);
    }
}