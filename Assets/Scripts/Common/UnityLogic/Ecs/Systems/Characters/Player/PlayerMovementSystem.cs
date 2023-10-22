using Common.Infrastructure.Services.Input;
using Common.UnityLogic.Ecs.Components.Characters;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Systems.Characters.Player
{
    public sealed class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTagComponent, MovableComponent> _movableFilter = null;

        private IInputService _inputService;
        private Transform _mainCameraTransform;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _mainCameraTransform = Camera.main.transform;
        }

        public void Run()
        {
            if (!_inputService.IsActive) return;

            foreach (var i in _movableFilter)
            {
                ref var movableComponent = ref _movableFilter.Get2(i);
                var axis = _inputService.Axis;
                var cameraForward = _mainCameraTransform.forward;
                cameraForward.y = 0.0f;
                cameraForward.Normalize();

                var cameraRight = _mainCameraTransform.right;
                cameraRight.y = 0.0f;
                cameraRight.Normalize();

                var movementDirection = (cameraRight * axis.x) + (cameraForward * axis.y);

                movableComponent.CharacterController.Move(movementDirection * movableComponent.Speed * Time.deltaTime);
            }
        }
    }
}