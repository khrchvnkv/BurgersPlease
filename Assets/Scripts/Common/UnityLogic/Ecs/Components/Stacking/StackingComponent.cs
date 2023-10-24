using Common.UnityLogic.Items;

namespace Common.UnityLogic.Ecs.Components.Stacking
{
    public struct StackingComponent
    {
        public StackingComponent(Item.ItemTypes itemType)
        {
            ItemType = itemType;
        }
        
        public Item.ItemTypes ItemType { get; }
    }
}