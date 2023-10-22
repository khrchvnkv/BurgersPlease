using System;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Components.Zones
{
    [Serializable]
    public struct GivingItemsZoneComponent
    {
        [field: SerializeField] public GameObject GivingItem;
    }
}