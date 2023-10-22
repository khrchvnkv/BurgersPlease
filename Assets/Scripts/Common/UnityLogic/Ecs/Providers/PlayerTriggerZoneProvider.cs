using Common.UnityLogic.Triggers;
using UnityEngine;
using Voody.UniLeo;

namespace Common.UnityLogic.Ecs.Providers
{
    [RequireComponent(typeof(PlayerTrigger))]
    public abstract class PlayerTriggerZoneProvider<T> : MonoProvider<T> where T : struct
    {
        [SerializeField] private PlayerTrigger _trigger;

        protected virtual void OnValidate() => _trigger ??= gameObject.GetComponent<PlayerTrigger>();
        protected virtual void OnEnable()
        {
            _trigger.OnEntered += OnPlayerEntered;
            _trigger.OnExited += OnPlayerExited;
        }
        protected virtual void OnDisable()
        {
            _trigger.OnEntered -= OnPlayerEntered;
            _trigger.OnExited -= OnPlayerExited;
        }
        protected virtual void OnPlayerEntered() { }
        protected virtual void OnPlayerExited() { }
    }
}