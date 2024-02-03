using System;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Popups.SkinsShop;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.Supplier
{
    public sealed class PopupSupplierAsync : FrameSupplierAsync<PopupName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public PopupSupplierAsync(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override async UniTask<UnityFrame> InstantiateFrame(PopupName key)
        {
            switch (key)
            {
                case PopupName.None:
                    break;
                case PopupName.SKINS_SHOP:
                    SkinsShopViewPopup skinsShopView = await uiFactory.CreateSkinsShopView();
                    skinsShopView.transform.SetParent(uiFactory.UIRoot.ContainerPopup, false);
                    skinsShopView.name = "SkinsShop";
                    return skinsShopView;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}