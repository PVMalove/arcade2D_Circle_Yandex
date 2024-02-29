using System.Collections.Generic;
using Code.Infrastructure.Services.Pool;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.Pool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2
{
    public class ShopItemsPresenter : MonoBehaviour
    {
        [SerializeField] private Transform itemContainer;
        [SerializeField] private Transform poolContainer;

        private List<ShopItemView> activeViews = new List<ShopItemView>();
        private ObjectPool<ShopItemView> objectPool;

        private IStaticDataService staticDataService;
        private IPersistentProgressStorage progressService;
        private PrefabFactory prefabFactory;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressStorage progressService,
            PrefabFactory prefabFactory)
        {
            this.staticDataService = staticDataService;
            this.progressService = progressService;
            this.prefabFactory = prefabFactory;
        }

        public async UniTask InitializeAsync()
        {
            objectPool = new ObjectPool<ShopItemView>(prefabFactory);
            PoolObjectConfig poolConfig = staticDataService.GetPoolConfigByType(PoolObjectType.ShopViewItem);
            await objectPool.InitializeAsync(poolConfig.AssetReference, poolConfig.StartCapacity,
                poolConfig.Type, poolContainer);
        }

        public async UniTask SetItems(IEnumerable<SkinShopItem> items)
        {
            foreach (SkinShopItem shopItem in items)
            {
                ShopItemView viewItem = await objectPool.Get(itemContainer.position, itemContainer);
                SetItem(viewItem, shopItem.Name, shopItem.Icon, shopItem.RequiredCoins);

                if (!progressService.Progress.PlayerItems.IsPlayerOwnCircleHeroSkin(shopItem.CircleHeroReference))
                {
                    continue;
                }

                viewItem.Unlock();
                // if (progressService.Progress.PlayerItems.SelectedCircleHeroDataReference.AssetGUID ==
                //     shopItem.CircleHeroReference.AssetGUID)
                // {
                //     viewItem.Select();
                // }
            }
        }

        public void Cleanup()
        {
            foreach (ShopItemView item in activeViews)
            {
                objectPool.Return(item);
            }

            activeViews.Clear();
        }

        private void SetItem(ShopItemView viewItem, string itemName, Sprite itemIcon, int requiredCoinsAmount)
        {
            viewItem.gameObject.SetActive(false);

            viewItem.SetItem(itemName, itemIcon, requiredCoinsAmount);
            viewItem.Unselect();
            viewItem.Lock();

            viewItem.gameObject.SetActive(true);
            activeViews.Add(viewItem);
        }
    }
}