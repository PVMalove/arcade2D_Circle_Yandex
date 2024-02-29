using CodeBase.Core.Data;

namespace CodeBase.Core.Services.ProgressService
{
    public interface IProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}