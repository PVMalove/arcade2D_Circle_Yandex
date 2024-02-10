using CodeBase.UI.HUD.Base;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.HUD.BuildInfo
{
    public class BuildInfoViewHUD : HUDBase<IBuildInfoPresenter>
    {
        [SerializeField] private TextMeshProUGUI textBuildNumber;

        protected override void Initialize(IBuildInfoPresenter presenter)
        {
            base.Initialize(presenter);
            FillData(presenter);
        }

        private void FillData(IBuildInfoPresenter config)
        {
            textBuildNumber.text = config.BuildNumber;
        }

        public class Factory : PlaceholderFactory<BuildInfoViewHUD>
        {
        }

        public override void OnShow(object args)
        {
            throw new System.NotImplementedException();
        }

        public override void OnHide()
        {
            throw new System.NotImplementedException();
        }
    }
}