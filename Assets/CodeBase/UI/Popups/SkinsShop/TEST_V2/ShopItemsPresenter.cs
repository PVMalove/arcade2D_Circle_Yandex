using System.Collections.Generic;
using Code.Infrastructure.Services.Pool;
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
        
        private List<ShopItemView> activeViews;
        private ObjectPool<ShopItemView> objectPool;
        
        private IStaticDataService staticDataService;
        
        [Inject]
        private void Construct(PrefabFactory prefabFactory, IStaticDataService staticDataService)
        {
            objectPool = new ObjectPool<ShopItemView>(prefabFactory);
            activeViews = new List<ShopItemView>();
            this.staticDataService = staticDataService;
        }
        
        public async UniTask InitializeAsync()
        {
            PoolObjectConfig poolConfig = staticDataService.GetPoolConfigByType(PoolObjectType.ShopViewItem);
            await objectPool.InitializeAsync(poolConfig.AssetReference, poolConfig.StartCapacity,
                poolConfig.Type, poolContainer);
        }
        
        public async UniTask SetItems(IEnumerable<SkinShopItem> items)
        {
            Cleanup();
            
            foreach (SkinShopItem shopItem in items)
            {
                ShopItemView viewItem = await objectPool.Get(itemContainer.position, itemContainer);
                SetItem(viewItem, shopItem.Name, shopItem.Icon, shopItem.RequiredCoins);
            }
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

        private void Cleanup()
        {
            foreach (ShopItemView item in activeViews)
            {
                objectPool.Return(item);
            }
            activeViews.Clear();
        }
    }
}