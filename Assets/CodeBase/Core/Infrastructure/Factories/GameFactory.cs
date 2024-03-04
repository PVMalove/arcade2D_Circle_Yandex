using System.Collections.Generic;
using CodeBase.Core.Services.PauseService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.Gameplay.Environment;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.Gameplay;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.StaticData;
using CodeBase.UI.Root;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Core.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();

        public GameObject CircleBackground { get; private set; }
        
        private readonly IStaticDataService staticDataService;
        private readonly IPauseService pauseService;
        private readonly IPersistentProgressService progressService;
        private readonly HUDRoot.Factory hudFactory;
        private readonly CircleBackground.Factory circleBackgroundFactory;
        private readonly CircleHero.Factory circleHeroFactory;

        public GameFactory(IStaticDataService staticDataService,
            IPauseService pauseService,
            IPersistentProgressService progressService,
            HUDRoot.Factory hudFactory,
            CircleBackground.Factory circleBackgroundFactory,
            CircleHero.Factory circleHeroFactory)
        {
            this.staticDataService = staticDataService;
            this.pauseService = pauseService;
            this.progressService = progressService;
            this.hudFactory = hudFactory;
            this.circleBackgroundFactory = circleBackgroundFactory;
            this.circleHeroFactory = circleHeroFactory;
        }

        // public GameObject CreateCircleHero()
        // {
        //     AssetReferenceT<CircleHeroData> player = progressStorage.Progress.PlayerItems.SelectedCircleHeroDataReference;
        //     GameObject circleHero = circleHeroFactory.Create(player).GameObject();
        //     return circleHero;
        // }
        
        public GameObject CreateHUD()
        {
            GameObject hudRoot = hudFactory.Create().GameObject();
            return hudRoot;
        }
        
        public GameObject CreateCircleBackground()
        {
            CircleBackground = circleBackgroundFactory.Create().GameObject();
            return CircleBackground;
        }

        private void Register(GameObject gameObject)
        {
            RegisterPauseHandler(gameObject);
            RegisterProgressWatchers(gameObject);
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (IProgressReader progressReader in gameObject.GetComponentsInChildren<IProgressReader>())
                RegisterProgress(progressReader);
        }
        
        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private void RegisterPauseHandler(GameObject gameObject)
        {
            foreach (IPauseHandler pauseHandler in gameObject.GetComponentsInChildren<IPauseHandler>())
                pauseService.Register(pauseHandler);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}