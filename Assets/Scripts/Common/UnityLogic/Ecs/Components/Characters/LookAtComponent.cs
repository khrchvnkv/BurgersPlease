using System;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Components.Characters
{
    [Serializable]
    public struct LookAtComponent
    {
        [field: SerializeField] public Transform LookAtTarget;
    }
}