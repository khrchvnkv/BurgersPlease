using Common.Infrastructure.Factories.UIFactory;
using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public class StandaloneInputService : MobileInputService
    {
        public override Vector2 Axis
        {
            get
            {
                var baseInput = base.Axis;
                if (!IsActive || baseInput != Vector2.zero) return baseInput;

                return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
            }
        }
        
        public StandaloneInputService(IUIFactory uiFactory) : base(uiFactory)
        { }
    }
}