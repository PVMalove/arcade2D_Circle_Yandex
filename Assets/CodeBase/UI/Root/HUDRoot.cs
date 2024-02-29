using CodeBase.Audio.Service;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.PauseService;
using CodeBase.Core.Services.ProgressService;
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
        //TODO DELETE CLASS
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

        public class Factory : PlaceholderFactory<HUDRoot> { }
    }
}