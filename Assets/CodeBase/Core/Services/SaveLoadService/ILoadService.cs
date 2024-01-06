using System;
using CodeBase.Core.Data;

namespace CodeBase.Core.Services.SaveLoadService
{
    public interface ILoadService
    {
        void LoadProgress();
        void Subscribe(Action<PlayerProgress> onComplete);
        void Unsubscribe(Action<PlayerProgress> onComplete);
    }
}