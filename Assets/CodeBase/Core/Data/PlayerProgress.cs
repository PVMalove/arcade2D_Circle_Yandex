using System;
using UnityEngine.Serialization;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        public PlayerItemsData PlayerItemsData;
        public CoinData CoinData;
        
        public PlayerProgress(AudioControlData audioControlData,
            PlayerItemsData playerItemsData, CoinData coinData)
        {
            AudioControlData = audioControlData;
            PlayerItemsData = playerItemsData;
            CoinData = coinData;
        }
    }
}