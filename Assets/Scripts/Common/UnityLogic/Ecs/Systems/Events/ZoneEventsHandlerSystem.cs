using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Ecs.Events;
using Common.UnityLogic.Ecs.Providers.Character;
using Leopotam.Ecs;

namespace Common.UnityLogic.Ecs.Systems.Events
{
    public class ZoneEventsHandlerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GivingItemsZoneEnteredEvent> _givingItemsEnteredFilter = null;
        private readonly EcsFilter<GivingItemsZoneExitedEvent> _givingItemsExitedFilter = null;
        private readonly EcsFilter<PickingUpItemsZoneEnteredEvent> _pickingUpItemsEnteredFilter = null;
        private readonly EcsFilter<PickingUpItemsZoneExitedEvent> _pickingUpItemsExitedFilter = null;
        
        private readonly EcsFilter<PlayerTagComponent> _playerFilter = null;

        public void Run()
        {
            UpdateGivingItemsZone();
            UpdatePickingUpItemsZone();
        }

        private void UpdateGivingItemsZone()
        {
            foreach (var i in _givingItemsExitedFilter)
            {
                foreach (var j in _playerFilter)
                {
                    var entity = _playerFilter.GetEntity(j);
                    entity.Del<StackingComponent>();
                    entity.Del<BlockDurationComponent>();
                }
            }
            
            foreach (var i in _givingItemsEnteredFilter)
            {
                foreach (var j in _playerFilter)
                {
                    var entity = _playerFilter.GetEntity(j);
                    entity.Get<StackingComponent>();
                }
            }
        }
        
        private void UpdatePickingUpItemsZone()
        {
            foreach (var i in _pickingUpItemsExitedFilter)
            {
                foreach (var j in _playerFilter)
                {
                    var entity = _playerFilter.GetEntity(j);
                    entity.Del<UnstackingComponent>();
                    entity.Del<BlockDurationComponent>();
                }
            }
            
            foreach (var i in _pickingUpItemsEnteredFilter)
            {
                foreach (var j in _playerFilter)
                {
                    var entity = _playerFilter.GetEntity(j);
                    entity.Get<UnstackingComponent>();
                }
            }
        }
    }
}