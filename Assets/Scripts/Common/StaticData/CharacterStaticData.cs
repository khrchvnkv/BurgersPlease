using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "CharacterStaticData", menuName = "Static Data/CharacterStaticData")]
    public class CharacterStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public CharacterData Data { get; private set; }
        [field: SerializeField] public bool IsDefault { get; private set; }
    }
}