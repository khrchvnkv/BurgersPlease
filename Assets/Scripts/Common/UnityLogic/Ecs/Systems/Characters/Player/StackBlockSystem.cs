using Common.UnityLogic.Ecs.Providers.Character;
using Leopotam.Ecs;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Systems.Characters.Player
{
    public class StackBlockSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BlockDurationComponent> _blockFilter = null;

        public void Run()
        {
            foreach (var i in _blockFilter)
            {
                ref var blockComponent = ref _blockFilter.Get1(i);
                blockComponent.Timer -= Time.deltaTime;

                if (!(blockComponent.Timer <= 0.0f)) continue;
                
                var entity = _blockFilter.GetEntity(i);
                entity.Del<BlockDurationComponent>();
            }
        }
    }
}