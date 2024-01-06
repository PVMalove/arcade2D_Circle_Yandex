using Zenject;

namespace CodeBase.UI.HUD.BuildInfo
{
    public class BuildInfoPresenter : IBuildInfoPresenter
    {
        private readonly BuildInfoConfig config;

        public BuildInfoPresenter(BuildInfoConfig config)
        {
            this.config = config;
        }

        public string BuildNumber => $"Build: [{config.BuildNumber}]";

        public sealed class Factory : PlaceholderFactory<BuildInfoConfig, IBuildInfoPresenter>
        {
        }
    }
}