using System;
using CodeBase.UI.HUD.Base;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.HUD.Supplier
{
    public class HUDSupplier : FrameSupplier<HUDName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public HUDSupplier(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override UnityFrame InstantiateFrame(HUDName key)
        {
            switch (key)
            {
                case HUDName.None:
                    break;
                case HUDName.BUILD_INFO:
                    BuildInfoViewHUD buildInfoView = uiFactory.CreateBuildInfoView();
                    buildInfoView.transform.SetParent(uiFactory.UIRoot.ContainerHUD, false);
                    return buildInfoView;
                case HUDName.SETTING_BAR:
                    SettingBarViewHUD settingBarView = uiFactory.CreateSettingBarView();
                    settingBarView.transform.SetParent(uiFactory.UIRoot.ContainerHUD, false);
                    return settingBarView;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }

            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}