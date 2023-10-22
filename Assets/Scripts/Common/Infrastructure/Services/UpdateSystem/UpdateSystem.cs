using System;
using UnityEngine;

namespace Common.Infrastructure.Services.UpdateSystem
{
    public sealed class UpdateSystem : MonoBehaviour, IUpdateSystem
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private void Update() => OnUpdate?.Invoke();
        private void FixedUpdate() => OnFixedUpdate?.Invoke();
        private void LateUpdate() => OnLateUpdate?.Invoke();
    }
}