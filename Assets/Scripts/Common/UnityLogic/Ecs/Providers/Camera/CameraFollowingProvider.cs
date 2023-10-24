using Common.Infrastructure.Services.ECS;
using Common.UnityLogic.Ecs.Components.Camera;
using Common.UnityLogic.Ecs.Components.Characters;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Camera
{
    public class CameraFollowingProvider : MonoBehaviour
    {
        [SerializeField] private TransformComponent _transformComponent;
        [SerializeField] private CameraFollowingComponent _cameraFollowingComponent;

        private IEcsStartup _ecsStartup;
        
        [Inject]
        private void Construct(IEcsStartup ecsStartup)
        {
            _ecsStartup = ecsStartup;
            
            Init();
        }
        private void Init()
        {
            var entity = _ecsStartup.World.NewEntity();

            // Transform Component
            entity.Get<TransformComponent>() = _transformComponent;
            
            // Camera Following Component
            entity.Get<CameraFollowingComponent>() = _cameraFollowingComponent;
        }
    }
}