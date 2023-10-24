using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Ecs.Components.Stacking;
using Leopotam.Ecs;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Systems
{
    public class BlockTimerSystemSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BlockStackDurationComponent, StackDataComponent> _blockStackFilter = null;
        private readonly EcsFilter<BlockUnstackDurationComponent> _blockUnstackFilter = null;

        public void Run()
        {
            foreach (var i in _blockStackFilter)
            {
                ref var stackingComponent = ref _blockStackFilter.Get2(i);
                if (stackingComponent.IsMaxCollected) continue;
                
                ref var blockComponent = ref _blockStackFilter.Get1(i);
                blockComponent.Timer -= Time.deltaTime;
                if (blockComponent.Timer <= 0.0f)
                {
                    var entity = _blockStackFilter.GetEntity(i);
                    if (entity != default) entity.Del<BlockStackDurationComponent>();
                }
            }
            
            foreach (var i in _blockUnstackFilter)
            {
                ref var blockComponent = ref _blockUnstackFilter.Get1(i);
                blockComponent.Timer -= Time.deltaTime;
                if (blockComponent.Timer <= 0.0f)
                {
                    var entity = _blockUnstackFilter.GetEntity(i);
                    if (entity != default) entity.Del<BlockUnstackDurationComponent>();
                }
            }
        }
    }
}