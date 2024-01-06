using System;
using System.Collections.Generic;
using CodeBase.Core.Services.PlayerProgressService;
using CodeBase.UI.HUD.BuildInfo;

namespace CodeBase.UI.HUD.Service
{
    public interface IHUDService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }
        
        void ShowBuildInfo(BuildInfoConfig config);
        void Cleanup();
        void ShowSettingBar();
    }
}