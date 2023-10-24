using Common.Infrastructure.Services.ECS;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers
{
    public abstract class MonoProvider : MonoBehaviour
    {
        protected EcsEntity Entity;

        private IEcsStartup _ecsStartup;
        
        [Inject]
        private void Inject(IEcsStartup startup) => _ecsStartup = startup;
        protected virtual void OnEnable() => EnableEntity();
        protected virtual void OnDisable() => DisableEntity();
        protected virtual void EnableEntity() => Entity = _ecsStartup.World.NewEntity();
        private void DisableEntity()
        {
            if (_ecsStartup.World.IsAlive()) Entity.Destroy();
        }
    }
}