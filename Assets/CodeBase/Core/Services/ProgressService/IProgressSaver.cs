using CodeBase.Core.Data;

namespace CodeBase.Core.Services.ProgressService
{
    public interface IProgressSaver : IProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}