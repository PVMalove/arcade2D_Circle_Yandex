namespace CodeBase.Core.Services.PauseService
{
    public interface IPauseHandler
    {
        void OnPauseChanged(bool isPaused);
    }
}