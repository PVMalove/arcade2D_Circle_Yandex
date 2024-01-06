using System.Collections.Generic;
using CodeBase.Core.Services.PauseService;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Root;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();

        public GameObject Sling { get; private set; }
        
        private readonly IStaticDataService staticDataService;
        private readonly IPauseService pauseService;
        private readonly HUDRoot.Factory hudFactory;

        public GameFactory(IStaticDataService staticDataService,
            IPauseService pauseService,
            HUDRoot.Factory hudFactory)
        {
            this.staticDataService = staticDataService;
            this.pauseService = pauseService;
            this.hudFactory = hudFactory;
        }
        
        public GameObject CreateHUD()
        {
            GameObject hudRoot = hudFactory.Create().GameObject();
            return hudRoot;
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