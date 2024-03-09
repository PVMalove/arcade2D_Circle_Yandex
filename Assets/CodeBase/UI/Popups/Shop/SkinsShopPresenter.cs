using System;
using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.StaticDataService;
using CodeBase.StaticData.UI.Shop;
using Zenject;

namespace CodeBase.UI.Popups.Shop
{
    public sealed class SkinsShopPresenter : ISkinsShopPresenter
    {
        public event Action ChangedCoinsAmount;
        public string CoinsAmount => 
            progressService.GetProgress().CoinData.CoinsAmount.ToString();

        public IReadOnlyCollection<SkinShopItem> SkinItems{ get; set; }

        private readonly IPersistentProgressService progressService;
        private readonly IStaticDataService staticDataService;

        public SkinsShopPresenter(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            this.progressService = progressService;
            this.staticDataService = staticDataService;
        }

        public void InitializeShopItems()
        {
            SkinItems = staticDataService.ShopItemsCatalog.SkinItems;
        }
        
        public void Subscribe()
        {
            progressService.CoinsAmountChanged += OnCoinsAmountChanged;
        }

        public void Unsubscribe()
        {
            progressService.CoinsAmountChanged -= OnCoinsAmountChanged;
        }

        private void OnCoinsAmountChanged()
        {
            ChangedCoinsAmount?.Invoke();
        }

        public sealed class Factory : PlaceholderFactory<ISkinsShopPresenter> { }
    }
}