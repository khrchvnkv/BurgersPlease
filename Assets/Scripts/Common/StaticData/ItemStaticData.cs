using Common.UnityLogic.Items;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "ItemStaticData", menuName = "Static Data/ItemStaticData")]
    public sealed class ItemStaticData : ScriptableObject
    {
        [field: SerializeField] public Item CreationItemPrefab { get; private set; }
    }
}