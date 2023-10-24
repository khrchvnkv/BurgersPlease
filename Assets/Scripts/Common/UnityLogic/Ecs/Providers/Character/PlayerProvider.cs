using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Character
{
    public sealed class PlayerProvider : MonoProvider
    {
        [SerializeField] private TransformComponent _transformComponent;
        [SerializeField] private LookAtComponent _lookAtComponent;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private AnimatorComponent _animatorComponent;
        [SerializeField] private StackDataComponent stackDataComponent;

        private IStaticDataService _staticDataService;
        
        [Inject]
        private void Construct(IStaticDataService staticDataService) => _staticDataService = staticDataService;
        protected override void EnableEntity()
        {
            base.EnableEntity();
            
            // Player Tag
            Entity.Get<PlayerTagComponent>();

            // Transform
            Entity.Get<TransformComponent>() = _transformComponent;
            
            // Look At
            Entity.Get<LookAtComponent>() = _lookAtComponent;
            
            // Animator
            Entity.Get<AnimatorComponent>() = _animatorComponent;
            
            // Movable
            var staticData = _staticDataService.GetCharacterStaticData();
            _movableComponent.Speed = staticData.Data.DefaultMovementSpeed;
            Entity.Get<MovableComponent>() = _movableComponent;
            
            // Stackable
            stackDataComponent.MaxStackValue = staticData.Data.DefaultStackCount;
            Entity.Get<StackDataComponent>() = stackDataComponent;
        }
    }
}