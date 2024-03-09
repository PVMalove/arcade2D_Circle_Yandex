using System;

namespace CodeBase.Core.Data
{
    [Serializable]
    public struct CoinData
    {
        public int CoinsAmount;

        public CoinData(int CoinsAmount)
        {
            this.CoinsAmount = CoinsAmount;
        }
    }
}