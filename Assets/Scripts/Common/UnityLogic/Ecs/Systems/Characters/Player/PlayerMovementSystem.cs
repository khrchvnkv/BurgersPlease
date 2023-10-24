using Common.Infrastructure.Services.Input;
using Common.UnityLogic.Ecs.Components.Camera;
using Common.UnityLogic.Ecs.Components.Characters;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Systems.Characters.Player
{
    public sealed class PlayerMovementSystem : IEcsRunSystem
    {
        private const float MOVEMENT_THRESHOLD = 0.1f;
        
        private readonly EcsFilter<PlayerTagComponent, MovableComponent, TransformComponent, AnimatorComponent> _movableFilter = null;
        private readonly EcsFilter<CameraFollowingComponent, TransformComponent> _cameraFilter = null;

        private IInputService _inputService;
        private Transform _mainCameraTransform;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Run()
        {
            if (!_inputService.IsActive) return;

            if (_mainCameraTransform is null)
            {
                foreach (var i in _cameraFilter)
                {
                    ref var cameraTransform = ref _cameraFilter.Get2(i);
                    _mainCameraTransform = cameraTransform.Transform;
                }
            }

            if (_mainCameraTransform is null) return;
            
            foreach (var i in _movableFilter)
            {
                ref var movableComponent = ref _movableFilter.Get2(i);
                ref var transformComponent = ref _movableFilter.Get3(i);
                ref var animationComponent = ref _movableFilter.Get4(i);
                
                var axis = _inputService.Axis;
                var cameraForward = _mainCameraTransform.forward;
                cameraForward.y = 0.0f;
                cameraForward.Normalize();

                var cameraRight = _mainCameraTransform.right;
                cameraRight.y = 0.0f;
                cameraRight.Normalize();
                
                var movementDirection = (cameraRight * axis.x) + (cameraForward * axis.y);
                var movementMagnitude = movementDirection.magnitude;
                
                if (movementDirection.sqrMagnitude >= MOVEMENT_THRESHOLD)
                {
                    movableComponent.CharacterController.Move(movementDirection * movableComponent.Speed * Time.deltaTime);
                    transformComponent.Transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    animationComponent.UpdateMovementSpeed(movementMagnitude);
                }
                else
                {
                    animationComponent.UpdateMovementSpeed(0.0f);
                }
            }
        }
    }
}