using System;
using System.Collections.Generic;

namespace CodeBase.Core.Data
{
    [Serializable]
    public struct PlayerItemsData
    {
        public List<string> SkinGuids;
        public string SelectedCircleHeroGuid;
        public PlayerItemsData(List<string> SkinGuids, string SelectedCircleHeroGuid)
        {
            this.SkinGuids = SkinGuids;
            this.SelectedCircleHeroGuid = SelectedCircleHeroGuid;
        }
    }
}