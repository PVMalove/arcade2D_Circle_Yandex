using CodeBase.Audio.Service;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameMode.States
{
    public class PlayGameModeState : IState
    {
        private readonly IAudioService audioService;
        private readonly ILogService log;

        public PlayGameModeState(IAudioService audioService,
            ILogService log)
        {
            this.audioService = audioService;
            this.log = log;
        }

        public UniTask Enter()
        {
            log.LogState("Enter", this);
            audioService.MusicSourceAudio.Play("mainMusic");
            return UniTask.CompletedTask;
        }
        
        public UniTask Exit()
        {
            log.LogState("Exit", this);
            return UniTask.CompletedTask;
        }
    }
}