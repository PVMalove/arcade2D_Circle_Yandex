using CodeBase.Audio.Service;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Services.Factories;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.GameFlow.GameHUD.State
{
    public class MainMenuState : IState
    {
        private readonly IUIFactory uiFactory;
        private readonly IHUDService hudService;
        private readonly IPersistentProgressStorage progressStorage;
        private readonly IAudioService audioService;
        private readonly ILogService log;

        public MainMenuState(IUIFactory uiFactory, IHUDService hudService, 
            IPersistentProgressStorage progressStorage,
            IAudioService audioService,
            ILogService log)
        {
            this.uiFactory = uiFactory;
            this.hudService = hudService;
            this.progressStorage = progressStorage;
            this.audioService = audioService;
            this.log = log;
        }

        public async UniTask Enter()
        {
            log.LogState("Enter", this);
            
            uiFactory.CreateUIRoot();
            hudService.ShowSettingBar();
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressStorage.Progress);
            audioService.MusicSourceAudio.Play("Example music");
        }

        public UniTask Exit()
        {
            foreach (IProgressSaver progressWriter in hudService.ProgressWriters) 
                progressWriter.UpdateProgress(progressStorage.Progress);
            
            log.LogState("Exit", this);
            return default;
        }
    }
}