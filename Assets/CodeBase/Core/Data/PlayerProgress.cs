using System;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        public string SelectedCircleHeroGuid;
        public PlayerOwnedItems PlayerItems;

        public PlayerProgress(AudioControlData audioControlData, string selectedCircleHeroGuid,
            PlayerOwnedItems playerItems)
        {
            AudioControlData = audioControlData;
            SelectedCircleHeroGuid = selectedCircleHeroGuid;
            PlayerItems = playerItems;
        }
    }
}