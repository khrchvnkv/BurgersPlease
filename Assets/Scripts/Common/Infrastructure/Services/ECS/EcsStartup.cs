using Common.Infrastructure.Services.UpdateSystem;
using Common.UnityLogic.Ecs.Systems;
using Common.UnityLogic.Ecs.Systems.Characters.Player;
using Common.UnityLogic.Ecs.Systems.Zones;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Services.ECS
{
    public sealed class EcsStartup : MonoBehaviour, IEcsStartup
    {
        private DiContainer _diContainer;
        private IUpdateSystem _monoUpdateSystem;

        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private EcsSystems _lateUpdateSystems;
        
        public EcsWorld World { get; private set; }

        [Inject]
        private void Construct(DiContainer diContainer, IUpdateSystem updateSystem)
        {
            _diContainer = diContainer;
            _monoUpdateSystem = updateSystem;
            
            Init();
        }
        public void SendMessage<T>(in T message) where T : struct => World.NewEntity().Get<T>() = message;
        private void Init()
        {
            World = new EcsWorld();
            
            _updateSystems = new EcsSystems(World);
            _fixedUpdateSystems = new EcsSystems(World);
            _lateUpdateSystems = new EcsSystems(World);
            
            AddInjections();
            AddSystems();
            AddOneFrames();
            
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
            _lateUpdateSystems.Init();

            _monoUpdateSystem.OnUpdate += UpdateEcs;
            _monoUpdateSystem.OnFixedUpdate += FixedUpdateEcs;
            _monoUpdateSystem.OnLateUpdate += LateUpdateEcs;
        }
        private void AddInjections()
        { }
        private void AddOneFrames()
        { }
        private void AddSystems()
        {
            // Update
            AddEcsSystem<PlayerMovementSystem>(_updateSystems);
            AddEcsSystem<CreationZoneSystem>(_updateSystems);
            AddEcsSystem<SalesZoneSystem>(_updateSystems);

            // FixedUpdate
            
            // LateUpdate
            AddEcsSystem<BlockTimerSystemSystem>(_lateUpdateSystems);
            AddEcsSystem<CameraFollowingThePlayerSystem>(_lateUpdateSystems);
        }
        private void AddEcsSystem<T>(EcsSystems systemsCollection) where T : class, IEcsRunSystem, new()
        {
            var system = new T();
            systemsCollection.Add(system);
            _diContainer.Inject(system);
        }
        private void AddOneFrame<T>() where T : struct
        {
            _updateSystems.OneFrame<T>();
            _fixedUpdateSystems.OneFrame<T>();
            _lateUpdateSystems.OneFrame<T>();
        }
        private void OnDestroy()
        {
            if (_monoUpdateSystem is not null)
            {
                _monoUpdateSystem.OnUpdate -= UpdateEcs;
                _monoUpdateSystem.OnFixedUpdate -= FixedUpdateEcs;
                _monoUpdateSystem.OnLateUpdate -= LateUpdateEcs;
            }

            _updateSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _lateUpdateSystems?.Destroy();
            World?.Destroy();
        }
        private void UpdateEcs() => _updateSystems?.Run();
        private void FixedUpdateEcs() => _fixedUpdateSystems?.Run();
        private void LateUpdateEcs() => _lateUpdateSystems?.Run();
    }
}