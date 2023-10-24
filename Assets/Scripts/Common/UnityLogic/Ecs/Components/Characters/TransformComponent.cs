using System;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Components.Characters
{
    [Serializable]
    public struct TransformComponent
    {
        [field: SerializeField] public Transform Transform { get; private set; }
    }
}