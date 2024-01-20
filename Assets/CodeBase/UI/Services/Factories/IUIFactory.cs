using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;
using CodeBase.UI.Windows.GameMenu;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Factories
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        BuildInfoViewHUD CreateBuildInfoView();
        SettingBarViewHUD CreateSettingBarView();
        UniTask<GameMenuViewWindow> CreateGameMenuView();
        void Cleanup();
        IUIRoot UIRoot { get; }
    }
}