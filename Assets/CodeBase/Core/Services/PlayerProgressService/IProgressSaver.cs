using CodeBase.Core.Data;

namespace CodeBase.Core.Services.PlayerProgressService
{
    public interface IProgressSaver : IProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}