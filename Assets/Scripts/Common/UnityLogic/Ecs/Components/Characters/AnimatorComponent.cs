using System;
using NaughtyAttributes;
using UnityEngine;

namespace Common.UnityLogic.Ecs.Components.Characters
{
    [Serializable]
    public struct AnimatorComponent
    {
        [SerializeField] private Animator _animator;
        [SerializeField, AnimatorParam(nameof(_animator))] private int _movementSpeedHash;

        public void UpdateMovementSpeed(float movementMagnitude)
        {
            movementMagnitude = Mathf.Clamp01(movementMagnitude);
            _animator.SetFloat(_movementSpeedHash, movementMagnitude);
        }
    }
}