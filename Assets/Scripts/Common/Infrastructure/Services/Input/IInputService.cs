using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        
        void ActivateInput();
        void DeactivateInput();
    }
}