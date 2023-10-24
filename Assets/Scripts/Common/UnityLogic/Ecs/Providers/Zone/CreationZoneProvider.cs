using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Ecs.Components.Stacking;
using Common.UnityLogic.Ecs.Components.Zones;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Zone
{
    public class CreationZoneProvider : PlayerTriggerZoneProvider
    {
        [SerializeField] private StackDataComponent stackDataComponent;
        
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) => _staticDataService = staticDataService;
        protected override void EnableEntity()
        {
            base.EnableEntity();
            
            // Zone Tag
            Entity.Get<CreationZoneTagComponent>();

            // Stack Component
            var item = _staticDataService.GameStaticData.ItemStaticData.CreationItemPrefab;
            var itemType = item.ItemType;
            stackDataComponent.StackingItem = item;
            stackDataComponent.MaxStackValue = _staticDataService.GameStaticData.ZonesStaticData.CreationZoneCapacity;
            stackDataComponent.StackingItem = item;
            Entity.Get<StackDataComponent>() = stackDataComponent;
            
            // Stacking Component
            Entity.Get<StackingComponent>() = new StackingComponent(itemType);
            
            // Block Duration
            Entity.Get<BlockStackDurationComponent>() =
                new BlockStackDurationComponent(_staticDataService.GameStaticData.ZonesStaticData.CreationTimer);
        }
        protected override void OnPlayerEntered()
        {
            base.OnPlayerEntered();
            Entity.Get<UnstackingComponent>();
        }
        protected override void OnPlayerExited()
        {
            base.OnPlayerExited();
            Entity.Del<UnstackingComponent>();
        }
    }
}