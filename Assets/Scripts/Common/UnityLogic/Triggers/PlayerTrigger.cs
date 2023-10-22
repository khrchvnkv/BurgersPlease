using System;
using Common.UnityLogic.Ecs.Providers.Character;
using UnityEngine;

namespace Common.UnityLogic.Triggers
{
    public class PlayerTrigger : MonoBehaviour
    {
        public event Action OnEntered;
        public event Action OnExited;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerTagProvider _))
            {
                OnEntered?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerTagProvider _))
            {
                OnExited?.Invoke();
            }
        }
    }
}