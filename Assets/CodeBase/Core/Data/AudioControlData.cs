using System;

namespace CodeBase.Core.Data
{
    [Serializable]
    public struct AudioControlData
    {
        public float AudioVolume;
        public bool MusicOn;
        public bool EffectsOn;
    }
}