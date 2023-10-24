using Common.UnityLogic.Ecs.Components.Camera;
using Common.UnityLogic.Ecs.Components.Characters;
using Leopotam.Ecs;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Providers.Camera
{
    public class CameraFollowingProvider : MonoProvider
    {
        [SerializeField] private TransformComponent _transformComponent;
        [SerializeField] private CameraFollowingComponent _cameraFollowingComponent;

        protected override void EnableEntity()
        {
            base.EnableEntity();
            
            // Transform Component
            Entity.Get<TransformComponent>() = _transformComponent;
            
            // Camera Following Component
            Entity.Get<CameraFollowingComponent>() = _cameraFollowingComponent;
        }
    }
}