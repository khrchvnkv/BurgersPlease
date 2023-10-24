using System.Linq;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Items;
using NTC.Pool;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Factories.ItemFactory
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly DiContainer _diContainer;
        private readonly ItemStaticData _itemStaticData;

        public ItemFactory(DiContainer diContainer, IStaticDataService staticDataService)
        {
            _diContainer = diContainer;
            _itemStaticData = staticDataService.GameStaticData.ItemStaticData;
        }
        public Item SpawnCreationItem(Item itemPrefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var item = NightPool.Spawn(itemPrefab, position, rotation, parent);
            _diContainer.Inject(item);
            return item;
        }
        public void DespawnCreationItem(Item item) => NightPool.Despawn(item);
        public void SpawnStackableCreationItem(ref StackDataComponent stackable)
        {
            var itemParent = stackable.ItemParent;
            var itemsCount = stackable.CurrentStackValue;
            var spawnPosition = itemParent.position + Vector3.up * itemsCount;
            var item = SpawnCreationItem(_itemStaticData.CreationItemPrefab, spawnPosition, itemParent.rotation, itemParent);
            stackable.StackingItems.Add(item);
        }
        public void DespawnStackableCreationItem(ref StackDataComponent stackable)
        {
            var item = stackable.StackingItems.Last();
            stackable.StackingItems.Remove(item);
            DespawnCreationItem(item);
        }
    }
}