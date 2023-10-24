using UnityEngine;

namespace Common.UnityLogic.Items
{
    public abstract class Item : MonoBehaviour
    {
        // Different item types
        public enum ItemTypes
        {
            CreationItem,
            ANOTHER_TYPE
        }
        
        public abstract ItemTypes ItemType { get; }
    }
}