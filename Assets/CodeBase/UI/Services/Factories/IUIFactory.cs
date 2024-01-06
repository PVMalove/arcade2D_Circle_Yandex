using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Root;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Factories
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        BuildInfoViewHUD CreateBuildInfoView();
        SettingBarViewHUD CreateSettingBarView();
        void Cleanup();
        IUIRoot UIRoot { get; }
    }
}