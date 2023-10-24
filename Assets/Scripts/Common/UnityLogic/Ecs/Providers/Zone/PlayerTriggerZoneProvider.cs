using Common.Infrastructure.Services.ECS;
using Common.UnityLogic.Triggers;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Zone
{
    [RequireComponent(typeof(PlayerTrigger))]
    public abstract class PlayerTriggerZoneProvider : MonoBehaviour
    {
        [SerializeField] private PlayerTrigger _trigger;

        private IEcsStartup _ecsStartup;
        protected EcsEntity Entity;
        
        [Inject]
        private void Inject(IEcsStartup startup) => _ecsStartup = startup;
        protected virtual void OnValidate() => _trigger ??= gameObject.GetComponent<PlayerTrigger>();
        protected virtual void OnEnable()
        {
            EnableEntity();
            _trigger.OnEntered += OnPlayerEntered;
            _trigger.OnExited += OnPlayerExited;
        }
        protected virtual void OnDisable()
        {
            DisableEntity();
            _trigger.OnEntered -= OnPlayerEntered;
            _trigger.OnExited -= OnPlayerExited;
        }
        protected virtual void OnPlayerEntered() { }
        protected virtual void OnPlayerExited() { }
        protected virtual void EnableEntity() => Entity = _ecsStartup.World.NewEntity();
        private void DisableEntity()
        {
            if (_ecsStartup.World.IsAlive()) Entity.Destroy();
        }
    }
}