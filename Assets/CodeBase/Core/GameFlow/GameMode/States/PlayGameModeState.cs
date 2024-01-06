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
            audioService.MusicSourceAudio.Play("mainMusic");
            return default;
        }
        
        public UniTask Exit()
        {
            return default;
        }
    }
}