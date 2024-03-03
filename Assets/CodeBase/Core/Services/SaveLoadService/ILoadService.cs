using CodeBase.Core.Data;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.SaveLoadService
{
    public interface ILoadService
    {
        UniTask<PlayerProgress> LoadProgress();
    }
}