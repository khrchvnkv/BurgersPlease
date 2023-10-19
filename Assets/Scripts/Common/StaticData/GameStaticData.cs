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
        [SerializeField, Expandable] private List<CharacterStaticData> _characterStaticDatas;
        [field: SerializeField] public WindowStaticData WindowStaticData { get; private set; }

        private void OnValidate()
        {
            if (_characterStaticDatas is null) return;
            
            var defaultCharactersCount = _characterStaticDatas.Count(x => x.IsDefault);
            if (defaultCharactersCount != 1)
                Debug.LogError("The character config setting is incorrect, 1 default character is required");
        }
        public CharacterStaticData GetDefaultCharacterStaticData() => 
            _characterStaticDatas.SingleOrDefault(x => x.IsDefault);
        public CharacterStaticData GetCharacterStaticData(in int skinIndex)
        {
            if (_characterStaticDatas.Count == 0 || skinIndex < 0 || skinIndex >= _characterStaticDatas.Count)
            {
                throw new ArgumentException($"Incorrect skin index value: {skinIndex}");
            }

            return _characterStaticDatas[skinIndex];
        }
    }
}