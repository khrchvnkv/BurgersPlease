using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Items;
using UnityEngine;

namespace Common.Infrastructure.Factories.ItemFactory
{
    public interface IItemFactory
    {
        Item SpawnCreationItem(Item itemPrefab, Vector3 position, Quaternion rotation, Transform parent);
        void DespawnCreationItem(Item item);
        void SpawnStackableCreationItem(ref StackDataComponent stackable);
        void DespawnStackableCreationItem(ref StackDataComponent stackable);
    }
}