using UnityEngine;

namespace Common.Infrastructure.Factories.CharacterFactory
{
    public interface ICharacterFactory
    {
        GameObject InstantiateCharacter(GameObject prefab, Transform parent);
    }
}