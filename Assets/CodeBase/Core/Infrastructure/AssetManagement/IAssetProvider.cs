using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace CodeBase.Core.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask InitializeAsync();
        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class;
        UniTask<TAsset> Load<TAsset>(string key) where TAsset : class;
        UniTask<List<string>> GetAssetsListByLabel<TAsset>(string label);
        UniTask<List<string>> GetAssetsListByLabel(string label, Type type = null);
        UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;
        UniTask WarmupAssetsByLabel(string label);
        UniTask ReleaseAssetsByLabel(string label);
        UniTask<TConfigObject> GetConfigFromResources<TConfigObject>(string name = "") where TConfigObject : UnityEngine.Object;
        void Cleanup();
    }
}