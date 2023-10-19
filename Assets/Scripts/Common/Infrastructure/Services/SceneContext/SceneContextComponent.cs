using Common.Infrastructure.Factories.CharacterFactory;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Services.SceneContext
{
    public sealed class SceneContextComponent : MonoBehaviour
    {
        [SerializeField] private Transform _characterSpawnPoint;

        private ISceneContextService _sceneContextService;

        [Inject]
        private void Construct(ISceneContextService sceneContextService)
        {
            _sceneContextService = sceneContextService;
            
            InitializeScene();
        }
        private void InitializeScene()
        {
            _sceneContextService.CharacterSpawnPoint = _characterSpawnPoint;
        }
    }
}