using System;
using UnityEngine;

namespace Common.Infrastructure.Services.SavedData
{
    [Serializable]
    public class SettingsData
    {
        public bool SoundOn;
        public bool MusicOn;

        public SettingsData()
        {
            SoundOn = true;
            MusicOn = true;
        }
    }
}