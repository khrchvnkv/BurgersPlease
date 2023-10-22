using UnityEngine;

namespace Common.StaticData
{
    [System.Serializable]
    public sealed class CharacterData
    {
        [field: SerializeField, Range(0.1f, 25.0f)] public float DefaultMovementSpeed { get; private set; }
        [field: SerializeField, Min(1)] public int DefaultStackCount { get; private set; }
    }
}