using CodeBase.Core.Data;

namespace CodeBase.Core.Services.ProgressService
{
    public interface IPersistentProgressStorage
    {
        PlayerProgress Progress { get; set; }
    }
}