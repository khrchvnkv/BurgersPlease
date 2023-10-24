using System;

namespace Common.Infrastructure.Services.SavedData
{
    [Serializable]
    public class ProgressData
    {
        public event Action<ulong> OnCurrencyValueChanged;
        
        public int? SelectedCharacterSkinIndex;
        public ulong HardCurrency;

        public void AddCurrency(ulong delta)
        {
            HardCurrency += delta;
            OnCurrencyValueChanged?.Invoke(HardCurrency);
        }
    }
}