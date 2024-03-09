using System;
using System.Collections.Generic;

namespace CodeBase.Core.Data
{
    [Serializable]
    public struct PlayerOwnedItemsData
    {
        public List<string> SkinGuids;
        
        public PlayerOwnedItemsData(List<string> SkinGuids)
        {
            this.SkinGuids = SkinGuids;
        }
    }
}