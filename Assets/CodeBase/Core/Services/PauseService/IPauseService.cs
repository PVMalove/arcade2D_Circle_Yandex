namespace CodeBase.Core.Services.PauseService
{
    public interface IPauseService
    {
        void Register(IPauseHandler pauseHandler);
        void Unregister(IPauseHandler pauseHandler);
        void SetPause(bool isPaused);
    }
}