using System;
using System.Collections.Generic;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerOwnedItems
    {
        public List<string> SkinGuids;
        

        public PlayerOwnedItems(List<string> SkinGuids)
        {
            this.SkinGuids = SkinGuids;
        }
    }
}