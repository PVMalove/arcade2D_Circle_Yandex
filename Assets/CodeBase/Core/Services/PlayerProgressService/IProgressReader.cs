using CodeBase.Core.Data;

namespace CodeBase.Core.Services.PlayerProgressService
{
    public interface IProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}