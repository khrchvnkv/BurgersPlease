using Common.Infrastructure.Factories.UIFactory;
using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        private readonly IUIFactory _uiFactory;
        
        private bool _isActive;
        
        public Vector2 Axis => 
            !_isActive ? Vector2.zero : new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        public MobileInputService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public void ActivateInput()
        {
            _isActive = true;
            UpdateJoystickAreaActivity();
        }
        public void DeactivateInput()
        {
            _isActive = false;
            UpdateJoystickAreaActivity();
        }
        private void UpdateJoystickAreaActivity() => _uiFactory.SetJoystickAreaActivity(_isActive);
    }
}