using CodeBase.Audio.Service;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.PauseService;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.Core.Services.SaveLoadService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Root
{
    public class HUDRoot : MonoBehaviour, IHUDRoot
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button clearProgressButton;
        [SerializeField] private Button openProductButton;
        [Header("Setting panel")] 
        [SerializeField] private GameObject settingsObject;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button musicSoundButton;
        [SerializeField] private Button sfxButton;
        [SerializeField] private GameObject musicOn;
        [SerializeField] private GameObject musicOff;
        [SerializeField] private GameObject sfxOn;
        [SerializeField] private GameObject sfxOff;
        
        private IPauseService pauseService;
        private IPersistentProgressStorage progressStorage;
        private IAudioService audioService;
        private VerticalLayoutGroup layoutGroup;
        private IPopupService popupService;
        private IStaticDataService staticData;
        private ILoadService loadService;
        private IGameFactory gameFactory;
        
        [Inject]
        private void Construct(IPauseService pauseService, IPersistentProgressStorage progressStorage,
            IAudioService audioService, IPopupService popupService,
            IStaticDataService staticData, ILoadService loadService, IGameFactory gameFactory)
        {
            this.pauseService = pauseService;
            this.progressStorage = progressStorage;
            this.audioService = audioService;
            this.popupService = popupService;
            this.staticData = staticData;
            this.loadService = loadService;
            this.gameFactory = gameFactory;
        }
        
        private void Awake()
        {
            pauseButton.onClick.AddListener(PauseGameClick);
        }

        private void Start()
        {
            layoutGroup = settingsObject.GetComponent<VerticalLayoutGroup>();
            clearProgressButton.onClick.AddListener(NewProgress);
        }

        
        private void PauseGameClick()
        {
            pauseService.SetPause(true);
        }
        
        
        private void ChangeMusicButtonImage(bool isOn)
        {
            musicOn.SetActive(isOn);
            musicOff.SetActive(!isOn);
        }

        private void ChangeSFXButtonImage(bool isOn)
        {
            sfxOn.SetActive(isOn);
            sfxOff.SetActive(!isOn);
        }
        
        private void NewProgress()
        {
            Debug.Log("Reset player progress");
            PlayerProgress progress = new();
            progress.AudioControlData.EffectsOn = true;
            progress.AudioControlData.MusicOn = true;
            progress.AudioControlData.AudioVolume = 0.5f;
            progressStorage.Progress = progress;
            
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressStorage.Progress);
        }

        public class Factory : PlaceholderFactory<HUDRoot>
        {
            
        }
    }
}