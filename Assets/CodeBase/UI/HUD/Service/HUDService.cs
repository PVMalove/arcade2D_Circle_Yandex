using System.Collections.Generic;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.HUD.Base;
using CodeBase.UI.HUD.BuildInfo;
using CodeBase.UI.HUD.SettingBar;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.HUD.Service
{
    public class HUDService : IHUDService
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();

        private readonly IFrameSupplier<HUDName, UnityFrame> supplier;
        private readonly BuildInfoPresenter.Factory buildInfoPresenterFactory;
        private readonly SettingBarPresenter.Factory settingBarPresenterFactory;

        public HUDService(IFrameSupplier<HUDName, UnityFrame> supplier,
            BuildInfoPresenter.Factory buildInfoPresenterFactory,
            SettingBarPresenter.Factory settingBarPresenterFactory)
        {
            this.supplier = supplier;
            this.buildInfoPresenterFactory = buildInfoPresenterFactory;
            this.settingBarPresenterFactory = settingBarPresenterFactory;
        }

        public void ShowBuildInfo(BuildInfoConfig config)
        {
            if (supplier.LoadFrame(HUDName.BUILD_INFO) is BuildInfoViewHUD buildInfoView)
            {
                IBuildInfoPresenter presenter = buildInfoPresenterFactory.Create(config);
                buildInfoView.Show(presenter);
            }
        }
        
        public void ShowSettingBar()
        {
            if (supplier.LoadFrame(HUDName.SETTING_BAR) is SettingBarViewHUD settingBarView)
            {
                ISettingBarPresenter presenter = settingBarPresenterFactory.Create();
                RegisterProgress(presenter);
                settingBarView.Show(presenter);
            }
        }
        

        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}