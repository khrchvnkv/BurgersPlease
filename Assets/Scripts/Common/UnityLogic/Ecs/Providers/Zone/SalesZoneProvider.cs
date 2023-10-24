using Common.UnityLogic.Ecs.Components.Stacking;
using Common.UnityLogic.Ecs.Components.Zones;
using Common.UnityLogic.Items;
using Leopotam.Ecs;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Providers.Zone
{
    public class SalesZoneProvider : PlayerTriggerZoneProvider
    {
        [SerializeField] private Item.ItemTypes _saleItemType;

        protected override void EnableEntity()
        {
            base.EnableEntity();
            
            // Cash Register Tag
            Entity.Get<SalesZoneTagComponent>();
        }
        protected override void OnPlayerEntered()
        {
            base.OnPlayerEntered();
            Entity.Get<StackingComponent>() = new StackingComponent(_saleItemType);
        }
        protected override void OnPlayerExited()
        {
            base.OnPlayerExited();
            Entity.Del<StackingComponent>();
        }
    }
}