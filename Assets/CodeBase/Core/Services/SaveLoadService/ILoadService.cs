using System;
using CodeBase.Core.Data;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.SaveLoadService
{
    public interface ILoadService
    {
        void LoadProgress();
        UniTask Subscribe(Action<PlayerProgress> onComplete);
        UniTask Unsubscribe(Action<PlayerProgress> onComplete);
    }
}