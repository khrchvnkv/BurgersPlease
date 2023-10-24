using Common.UnityLogic.Triggers;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Providers.Zone
{
    [RequireComponent(typeof(PlayerTrigger))]
    public abstract class PlayerTriggerZoneProvider : MonoProvider
    {
        [SerializeField] private PlayerTrigger _trigger;
        
        protected virtual void OnValidate() => _trigger ??= gameObject.GetComponent<PlayerTrigger>();
        protected override void OnEnable()
        {
            base.OnEnable();
            _trigger.OnEntered += OnPlayerEntered;
            _trigger.OnExited += OnPlayerExited;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _trigger.OnEntered -= OnPlayerEntered;
            _trigger.OnExited -= OnPlayerExited;
        }
        protected virtual void OnPlayerEntered() { }
        protected virtual void OnPlayerExited() { }
    }
}