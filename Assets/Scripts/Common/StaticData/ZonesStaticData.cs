using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "ZonesStaticData", menuName = "Static Data/ZonesStaticData")]
    public class ZonesStaticData : ScriptableObject
    {
        [field: SerializeField, Min(0.1f)] public float CreationTimer;
        [field: SerializeField, Min(1)] public int CreationZoneCapacity;

        [Space] 
        [field: SerializeField, Min(0)] public ulong SalePrice;
    }
}