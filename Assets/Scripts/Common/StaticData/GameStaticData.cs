using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Static Data/GameStaticData")]
    public class GameStaticData : ScriptableObject
    {
        [field: Header("GAMEPLAY DATA")]
        [field: SerializeField, Expandable] public StackingStaticData StackingStaticData { get; private set; }
        [SerializeField, Expandable] private List<CharacterStaticData> _characterStaticDatas;

        [field: Header("UI DATA")]
        [field: SerializeField, Expandable] public WindowStaticData WindowStaticData { get; private set; }

        private void OnValidate()
        {
            var defaultCharactersCount = _characterStaticDatas.Count(x => x.IsDefault);
            if (defaultCharactersCount != 1)
                Debug.LogError("The character config setting is incorrect, 1 default character is required");
        }
        public CharacterStaticData GetDefaultCharacterStaticData() => 
            _characterStaticDatas.SingleOrDefault(x => x.IsDefault);
        public CharacterStaticData GetCharacterStaticData(in int? skinIndex)
        {
            if (!skinIndex.HasValue) throw new Exception($"Character skin index not set");

            var index = (int)skinIndex;
            if (_characterStaticDatas.Count == 0 || index < 0 || index >= _characterStaticDatas.Count)
            {
                throw new ArgumentException($"Incorrect skin index value: {index}");
            }

            return _characterStaticDatas[index];
        }
    }
}