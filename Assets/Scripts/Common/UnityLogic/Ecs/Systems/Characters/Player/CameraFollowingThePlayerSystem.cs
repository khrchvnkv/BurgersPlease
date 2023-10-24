using Common.UnityLogic.Ecs.Components.Camera;
using Common.UnityLogic.Ecs.Components.Characters;
using Leopotam.Ecs;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Systems.Characters.Player
{
    public class CameraFollowingThePlayerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraFollowingComponent> _cameraFollowingFilter = null;
        private readonly EcsFilter<PlayerTagComponent, LookAtComponent> _transformFilter = null;

        private Transform _cameraTransform;
        private Vector3 _offset;

        public void Run()
        {
            foreach (var i in _cameraFollowingFilter)
            {
                var entity = _cameraFollowingFilter.GetEntity(i);
                _cameraTransform = entity.Get<TransformComponent>().Transform;
                _offset = _cameraFollowingFilter.Get1(i).Offset;   
            }
            
            foreach (var i in _transformFilter)
            {
                ref var movable = ref _transformFilter.Get2(i);
                var target = movable.LookAtTarget;

                _cameraTransform.position = target.position + _offset;
                _cameraTransform.LookAt(target);
            }
        }
    }
}