using Common.Infrastructure.Factories.UIFactory;
using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        private readonly IUIFactory _uiFactory;
        
        public bool IsActive { get; private set; }
        public Vector2 Axis => 
            !IsActive ? Vector2.zero : new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        public MobileInputService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public void ActivateInput()
        {
            IsActive = true;
            UpdateJoystickAreaActivity();
        }
        public void DeactivateInput()
        {
            IsActive = false;
            UpdateJoystickAreaActivity();
        }
        private void UpdateJoystickAreaActivity() => _uiFactory.SetJoystickAreaActivity(IsActive);
    }
}