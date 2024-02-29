using System.Collections.Generic;
using CodeBase.Core.Services.PauseService;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.Gameplay.Environment;
using CodeBase.UI.Root;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();

        public GameObject CircleBackground { get; private set; }
        
        private readonly IStaticDataService staticDataService;
        private readonly IPauseService pauseService;
        private readonly HUDRoot.Factory hudFactory;
        private readonly CircleBackground.Factory circleBackgroundFactory;

        public GameFactory(IStaticDataService staticDataService,
            IPauseService pauseService,
            HUDRoot.Factory hudFactory,
            CircleBackground.Factory circleBackgroundFactory)
        {
            this.staticDataService = staticDataService;
            this.pauseService = pauseService;
            this.hudFactory = hudFactory;
            this.circleBackgroundFactory = circleBackgroundFactory;
        }
        
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