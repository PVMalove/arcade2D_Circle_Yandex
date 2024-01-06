using CodeBase.Core.Data;

namespace CodeBase.Core.Services.PlayerProgressService
{
    public interface IPersistentProgressStorage
    {
        PlayerProgress Progress { get; set; }
    }
}