using Common.Infrastructure.Services.ECS;
using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Character
{
    public sealed class PlayerProvider : MonoBehaviour
    {
        [SerializeField] private TransformComponent _transformComponent;
        [SerializeField] private LookAtComponent _lookAtComponent;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private AnimatorComponent _animatorComponent;
        [SerializeField] private StackDataComponent stackDataComponent;
        
        private IEcsStartup _ecsStartup;
        private IStaticDataService _staticDataService;
        
        [Inject]
        private void Construct(IEcsStartup ecsStartup, IStaticDataService staticDataService)
        {
            _ecsStartup = ecsStartup;
            _staticDataService = staticDataService;
            
            Init();
        }
        private void Init()
        {
            var entity = _ecsStartup.World.NewEntity();
            
            // Player Tag
            entity.Get<PlayerTagComponent>();

            // Transform
            entity.Get<TransformComponent>() = _transformComponent;
            
            // Look At
            entity.Get<LookAtComponent>() = _lookAtComponent;
            
            // Animator
            entity.Get<AnimatorComponent>() = _animatorComponent;
            
            // Movable
            var staticData = _staticDataService.GetCharacterStaticData();
            _movableComponent.Speed = staticData.Data.DefaultMovementSpeed;
            entity.Get<MovableComponent>() = _movableComponent;
            
            // Stackable
            stackDataComponent.MaxStackValue = staticData.Data.DefaultStackCount;
            entity.Get<StackDataComponent>() = stackDataComponent;
        }
    }
}