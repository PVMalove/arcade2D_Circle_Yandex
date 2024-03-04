using System;
using System.Collections.Generic;
using Code.Infrastructure.Services.Pool;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.Pool;
using CodeBase.UI.Popups.SkinsShop.TEST_V2.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2
{
    public class ShopItemsPresenter : MonoBehaviour
    {
        [SerializeField] private Transform itemContainer;
        [SerializeField] private Transform poolContainer;

        private readonly List<ShopItemView> activeViews = new List<ShopItemView>();
        private ObjectPool<ShopItemView> objectPool;

        private IStaticDataService staticDataService;
        private IPersistentProgressService progressService;
        private PrefabFactory prefabFactory;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistentProgressService progressService,
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

        public async UniTask SetSkinItems(IEnumerable<SkinShopItem> items)
        {
            foreach (SkinShopItem shopItem in items)
            {
                ShopItemView viewItem = await objectPool.Get(itemContainer.position, itemContainer);
                SetItem(viewItem, shopItem.Name, shopItem.Icon, shopItem.RequiredCoins,
                    () => BuySkinItem(shopItem.CircleHeroReference, shopItem.RequiredCoins),
                    () => SelectSkinItem(shopItem.CircleHeroReference));

                if (!progressService.IsPlayerOwnCircleHeroSkin(shopItem.CircleHeroReference))
                {
                    continue;
                }

                viewItem.Unlock();
                
                if (progressService.SelectedCircleDataReference.AssetGUID == shopItem.CircleHeroReference.AssetGUID)
                {
                    viewItem.Select();
                }
            }
        }

        private void BuySkinItem(AssetReferenceT<CircleHeroData> reference, int price)
        {
            progressService.OpenCircleHeroSkin(reference);
        }
        
        private void SelectSkinItem(AssetReferenceT<CircleHeroData> reference)
        {
            UnselectAllItems();
            progressService.SelectedCircleHeroSkin(reference);
        }

        public void Cleanup()
        {
            foreach (ShopItemView item in activeViews)
            {
                objectPool.Return(item);
            }

            activeViews.Clear();
        }

        private void SetItem(ShopItemView viewItem, string itemName, Sprite itemIcon, int requiredCoinsAmount,
            Action onBuyButtonClicked, Action onSelectButtonClicked)
        {
            viewItem.gameObject.SetActive(false);

            viewItem.SetItem(itemName, itemIcon, requiredCoinsAmount,
                onBuyButtonClicked, onSelectButtonClicked);
            
            viewItem.Unselect();
            viewItem.Lock();

            viewItem.gameObject.SetActive(true);
            activeViews.Add(viewItem);
        }
        
        private void UnselectAllItems()
        {
            foreach (ShopItemView shopItemView in activeViews)
            {
                if (shopItemView.IsSelected)
                {
                    shopItemView.Unselect();
                }
            }
        }
    }
}