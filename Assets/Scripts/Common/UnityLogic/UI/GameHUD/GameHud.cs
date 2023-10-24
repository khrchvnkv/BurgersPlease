using Common.Infrastructure.Services.Progress;
using Common.UnityLogic.UI.Windows;
using TMPro;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.UI.GameHUD
{
    public class GameHud : WindowBase<GameHudWindowData>
    {
        private const string CurrencyFormat = "${0}";
        
        [SerializeField] private TMP_Text _hardCurrencyText;
        
        private IPersistentProgressService _progressService;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }

        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();
            _progressService.SaveData.Progress.OnCurrencyValueChanged += UpdateHardCurrencyText;
            UpdateHardCurrencyText(_progressService.SaveData.Progress.HardCurrency);
        }
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            _progressService.SaveData.Progress.OnCurrencyValueChanged -= UpdateHardCurrencyText;
        }
        private void UpdateHardCurrencyText(ulong value) => 
            _hardCurrencyText.text = string.Format(CurrencyFormat, value);
    }
}