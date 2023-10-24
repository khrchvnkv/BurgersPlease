using Common.Infrastructure.StateMachine;
using Common.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Common.Infrastructure
{
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            Init();
        }
        private void Init() => _gameStateMachine.Enter<BootstrapState>();
    }
}