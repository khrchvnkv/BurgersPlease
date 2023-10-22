using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public interface IInputService
    {
        bool IsActive { get; }
        Vector2 Axis { get; }
        
        void ActivateInput();
        void DeactivateInput();
    }
}