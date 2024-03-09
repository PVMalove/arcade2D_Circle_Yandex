using System;
using UnityEngine.Serialization;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        public string SelectedCircleHeroGuid;
        public PlayerOwnedItemsData PlayerItemsData;
        public CoinData CoinData;
        
        public PlayerProgress(AudioControlData audioControlData, string selectedCircleHeroGuid,
            PlayerOwnedItemsData playerItemsData, CoinData coinData)
        {
            AudioControlData = audioControlData;
            SelectedCircleHeroGuid = selectedCircleHeroGuid;
            PlayerItemsData = playerItemsData;
            CoinData = coinData;
        }
    }
}