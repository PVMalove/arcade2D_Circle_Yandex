using System;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        
        public PlayerProgress()
        {
            AudioControlData = new AudioControlData();
        }
    }
}