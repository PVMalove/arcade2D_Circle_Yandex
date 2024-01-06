using CodeBase.Core.Data;

namespace CodeBase.Core.Services.PlayerProgressService
{
    public class PersistentProgressStorage : IPersistentProgressStorage
    {
        public PlayerProgress Progress { get; set; }
    }
}