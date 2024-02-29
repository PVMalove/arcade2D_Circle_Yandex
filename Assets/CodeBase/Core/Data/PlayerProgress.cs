using System;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        public PlayerOwnedItems PlayerItems;
        
        public PlayerProgress(PlayerOwnedItems playerItems, AudioControlData audioControlData)
        {
            PlayerItems = playerItems;
            AudioControlData = audioControlData;
        }
    }
}