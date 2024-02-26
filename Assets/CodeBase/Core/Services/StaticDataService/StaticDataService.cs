using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Services.Pool;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Services.LogService;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.UI;
using CodeBase.UI.Popups.SkinsShop.TEST_V2;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.Pool;
using CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.StaticDataService
{
    // This service incapsulate logic of uploading configs and give convenient API
    // for all consumers to receive necessary configs
    public class StaticDataService : IStaticDataService
    {
        public CharacterConfig CharacterConfig { get; private set; }
        public ScreensCatalog ScreensCatalog { get; private set; }
        public PopupsCatalog PopupsCatalog { get; private set; }
        //public SkinsItemCatalog SkinsItemCatalog { get; private set; }
        
        public ShopItemsCatalog ShopItemsCatalog { get; private set; }
        public PoolObjectConfig GetPoolConfigByType(PoolObjectType type) => poolObjectConfigsCache[type];

        private Dictionary<PoolObjectType, PoolObjectConfig> poolObjectConfigsCache;
        
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
                LoadPoolConfigs(),
                LoadScreensConfig(),
                LoadPopupsConfig(),
                LoadSkinsItemConfig(),
            };

            await UniTask.WhenAll(tasks);
            log.LogService("Static data loaded", this);
        }

        private async UniTask LoadScreensConfig()
        {
            ScreensCatalog[] configs = await GetConfigs<ScreensCatalog>();
            if (configs.Length > 0)
                ScreensCatalog = configs.First();
            else
                log.LogError("There are no screens config founded!");
        }
        
        private async UniTask LoadPopupsConfig()
        {
            PopupsCatalog[] configs = await GetConfigs<PopupsCatalog>();
            if (configs.Length > 0)
                PopupsCatalog = configs.First();
            else
                log.LogError("There are no popups config founded!");
        }
        
        // private async UniTask LoadSkinsItemConfig()
        // {
        //     SkinsItemCatalog[] configs = await GetConfigs<SkinsItemCatalog>();
        //     if (configs.Length > 0)
        //         SkinsItemCatalog = configs.First();
        //     else
        //         log.LogError("There are no skins config founded!");
        // }
        
        private async UniTask LoadPoolConfigs()
        {
            PoolObjectConfig[] configsList = await GetConfigs<PoolObjectConfig>();
            poolObjectConfigsCache = configsList.ToDictionary(config => config.Type, config => config);
        }
        
        private async UniTask LoadSkinsItemConfig()
        {
            ShopItemsCatalog[] configs = await GetConfigs<ShopItemsCatalog>();
            if (configs.Length > 0)
                ShopItemsCatalog = configs.First();
            else
                log.LogError("There are no shop items config founded!");
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