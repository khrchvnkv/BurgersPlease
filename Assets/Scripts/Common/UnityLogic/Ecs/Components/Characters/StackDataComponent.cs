using System;
using System.Collections.Generic;
using Common.UnityLogic.Ecs.Components.Stacking;
using Common.UnityLogic.Items;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Components.Characters
{
    [Serializable]
    public struct StackDataComponent
    {
        private List<Item> _stackingItems;

        public Item StackingItem { get; set; }
        [field: SerializeField] public Transform ItemParent { get; private set; }
        [field: SerializeField] public GameObject MaxItemCollectedLogo { get; private set; }
        public int CurrentStackValue => StackingItems.Count;
        public int MaxStackValue { get; set; }
        public List<Item> StackingItems => _stackingItems ??= new List<Item>();
        public bool IsMaxCollected => CurrentStackValue == MaxStackValue;
        public bool IsEmpty => CurrentStackValue == 0;

        public bool CanStackItemType(in Item itemType)
        {
            if (StackingItem == null) StackingItem = itemType;
            return StackingItem == itemType;
        }
        public bool CanUnstackToStack(ref StackingComponent stacking) => stacking.ItemType == StackingItem.ItemType;
        public void UpdateMaxCollectedLogo()
        {
            MaxItemCollectedLogo.SetActive(IsMaxCollected);
        }
        public void UpdateStackItemType()
        {
            if (IsEmpty) ResetStackingItemType();
        }
        private void ResetStackingItemType() => StackingItem = null;
    }
}