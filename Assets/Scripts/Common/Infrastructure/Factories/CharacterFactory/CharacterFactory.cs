using Common.Infrastructure.Factories.Zenject;
using UnityEngine;

namespace Common.Infrastructure.Factories.CharacterFactory
{
    public sealed class CharacterFactory : ICharacterFactory
    {
        private readonly IZenjectFactory _zenjectFactory;

        public CharacterFactory(IZenjectFactory zenjectFactory)
        {
            _zenjectFactory = zenjectFactory;
        }
        public GameObject InstantiateCharacter(GameObject prefab, Transform parent)
        {
            var character = _zenjectFactory.Instantiate(prefab, parent);
            return character;
        }
    }
}