using System;
using Common.Infrastructure.Services.UpdateSystem;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Billboard
{
    public sealed class Billboard : MonoBehaviour
    {
        private IUpdateSystem _updateSystem;
        private Transform _mainCameraTransform;
        private Transform _billboardTransform;

        [Inject]
        private void Construct(IUpdateSystem updateSystem)
        {
            _updateSystem = updateSystem;
            _mainCameraTransform = Camera.main.transform;
            _billboardTransform = transform;
        }

        private void OnEnable() => _updateSystem.OnLateUpdate += UpdateBillboard;
        private void OnDisable() => _updateSystem.OnLateUpdate -= UpdateBillboard;
        private void UpdateBillboard() => _billboardTransform.LookAt(_mainCameraTransform);
    }
}