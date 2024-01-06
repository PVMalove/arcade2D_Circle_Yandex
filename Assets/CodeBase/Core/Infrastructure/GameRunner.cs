using CodeBase.Core.Services.LogService;
using UnityEngine;
using YG;
using Zenject;

namespace CodeBase.Core.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private YandexGame yandexGame;
        private GameBootstrapper.Factory bootstrapperFactory;
        private ILogService log;

        [Inject]
        void Construct(GameBootstrapper.Factory bootstrapperFactory, ILogService log)
        {
            this.bootstrapperFactory = bootstrapperFactory;
            this.log = log;
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadCompleteYandexSDK;
        }

        private void LoadCompleteYandexSDK()
        {
            if (YandexGame.SDKEnabled)
            {
                log.LogYandex("Load complete yandex SDK", this);
                GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();
                if(bootstrapper != null) return;
                
                bootstrapperFactory.Create();
                
                Destroy(gameObject);
            }
        }

        private void Awake()
        {
            YandexGame loadingYandexSDK = FindObjectOfType<YandexGame>();
            if(loadingYandexSDK != null) return;

            Instantiate(yandexGame);
        }

        private void OnDisable() => 
            YandexGame.GetDataEvent -= LoadCompleteYandexSDK;
    }
}