using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Services.LogService;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.UI;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.StaticDataService
{
    // This service incapsulate logic of uploading configs and give convenient API
    // for all consumers to receive necessary configs
    public class StaticDataService : IStaticDataService
    {
        public CharacterConfig CharacterConfig { get; private set; }
        public WindowsConfig WindowsConfig { get; private set; }
        
        private readonly ILogService log;
        private readonly IAssetProvider assetProvider;

        public StaticDataService(ILogService log, IAssetProvider assetProvider)
        {
            this.log = log;
            this.assetProvider = assetProvider;
        }

        public async UniTask InitializeAsync()
        {
            // load your configs here
            List<UniTask> tasks = new List<UniTask>
            {
                LoadWindowsConfig(),
            };

            await UniTask.WhenAll(tasks);
            log.LogService("Static data loaded", this);
        }

        private async UniTask LoadWindowsConfig()
        {
            WindowsConfig[] configs = await GetConfigs<WindowsConfig>();
            if (configs.Length > 0)
                WindowsConfig = configs.First();
            else
                log.LogError("There are no character config founded!");
        }
        
        private async UniTask<List<string>> GetConfigKeys<TConfig>() => 
            await assetProvider.GetAssetsListByLabel<TConfig>(AssetLabels.Configs);

        private async UniTask<TConfig[]> GetConfigs<TConfig>() where TConfig : class
        {
            List<string> keys = await GetConfigKeys<TConfig>();
            return await assetProvider.LoadAll<TConfig>(keys);
        }
    }
}