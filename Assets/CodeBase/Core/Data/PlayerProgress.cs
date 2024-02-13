using System;
using CodeBase.UI.Popups.SkinsShop.TEST.Data;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        public SkinData SkinData;
        
        public PlayerProgress()
        {
            AudioControlData = new AudioControlData();
            SkinData = new SkinData();
        }
    }
}