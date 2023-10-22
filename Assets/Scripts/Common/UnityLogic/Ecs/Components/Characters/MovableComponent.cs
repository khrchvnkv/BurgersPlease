using System;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Components.Characters
{
    [Serializable]
    public struct MovableComponent
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [HideInInspector] public float Speed { get; set; }
    }
}