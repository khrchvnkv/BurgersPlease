using Common.Infrastructure.Factories.ItemFactory;
using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Ecs.Components.Stacking;
using Common.UnityLogic.Ecs.Components.Zones;
using Leopotam.Ecs;
using Zenject;

namespace Common.UnityLogic.Ecs.Systems.Zones
{
    public class CreationZoneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CreationZoneTagComponent, StackDataComponent, StackingComponent>.Exclude<BlockStackDurationComponent> _stackingZoneFilter = null;
        private readonly EcsFilter<CreationZoneTagComponent, StackDataComponent, UnstackingComponent>.Exclude<BlockUnstackDurationComponent> _unstackingZoneFilter = null;

        private readonly EcsFilter<PlayerTagComponent, StackDataComponent> _playerFilter = null;

        private IItemFactory _itemFactory;
        
        private float _delayBtwZoneStack;
        private float _delayBtwZoneUnstack;

        [Inject]
        private void Construct(IItemFactory itemFactory, IStaticDataService staticDataService)
        {
            _itemFactory = itemFactory;
            
            _delayBtwZoneStack = staticDataService.GameStaticData.ZonesStaticData.CreationTimer;
            _delayBtwZoneUnstack = staticDataService.GameStaticData.StackingStaticData.StackingTimer;
        }
        public void Run()
        {
            Stacking();
            Unstacking();
        }
        private void Stacking()
        {
            foreach (var i in _stackingZoneFilter)
            {
                ref var stackable = ref _stackingZoneFilter.Get2(i);
                StackItem(ref stackable, i);
            }
        }
        private void Unstacking()
        {
            foreach (var i in _unstackingZoneFilter)
            {
                ref var stackable = ref _unstackingZoneFilter.Get2(i);
                UnstackItem(ref stackable, i);
            }
        }
        private void StackItem(ref StackDataComponent stackable, in int entityIndex)
        {
            if (stackable.IsMaxCollected) return;

            // Spawn Item
            _itemFactory.SpawnStackableCreationItem(ref stackable);
            var entity = _stackingZoneFilter.GetEntity(entityIndex);
            stackable.UpdateMaxCollectedLogo();
            entity.Get<BlockStackDurationComponent>() = new BlockStackDurationComponent(_delayBtwZoneStack);
        }
        private void UnstackItem(ref StackDataComponent stackable, in int entityIndex)
        {
            if (stackable.CurrentStackValue <= 0) return;

            foreach (var i in _playerFilter)
            {
                ref var playerStackable = ref _playerFilter.Get2(i);
                if (playerStackable.IsMaxCollected ||
                    !playerStackable.CanStackItemType(stackable.StackingItem)) return;
            }
            
            // Despawn Item
            _itemFactory.DespawnStackableCreationItem(ref stackable);
            var entity = _unstackingZoneFilter.GetEntity(entityIndex);
            stackable.UpdateMaxCollectedLogo();
            entity.Get<BlockUnstackDurationComponent>() = new BlockUnstackDurationComponent(_delayBtwZoneUnstack);

            foreach (var i in _playerFilter)
            {
                ref var playerStackable = ref _playerFilter.Get2(i);
                _itemFactory.SpawnStackableCreationItem(ref playerStackable);
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                if (playerEntity == default) return;
                playerStackable.UpdateMaxCollectedLogo();
            }
        }
    }
}