using CodeBase.Core.Data;

namespace CodeBase.Core.Services.ProgressService
{
    public class PersistentProgressStorage : IPersistentProgressStorage
    {
        public PlayerProgress Progress { get; set; }
    }
}