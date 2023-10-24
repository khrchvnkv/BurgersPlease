using UnityEngine;

namespace Common.StaticData
{
    [System.Serializable]
    public sealed class CharacterData
    {
        [field: SerializeField, Min(0.1f)] public float DefaultMovementSpeed { get; private set; }
        [field: SerializeField, Min(1)] public int DefaultStackCount { get; private set; }
    }
}