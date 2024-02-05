using System;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;
using CodeBase.UI.Windows.Base;
using CodeBase.UI.Windows.GameMenu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Windows.Supplier
{
    public class WindowSupplierAsync : FrameSupplierAsync<ScreenName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public WindowSupplierAsync(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override async UniTask<UnityFrame> InstantiateFrame(ScreenName key)
        {
            switch (key)
            {
                case ScreenName.None:
                    break;
                case ScreenName.GAME_MENU:
                    GameMenuViewScreen gameMenuView = await uiFactory.CreateGameMenuView();
                    gameMenuView.transform.SetParent(uiFactory.UIRoot.ContainerScreen, false);
                    gameMenuView.name = "GameMenu";
                    return gameMenuView;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}