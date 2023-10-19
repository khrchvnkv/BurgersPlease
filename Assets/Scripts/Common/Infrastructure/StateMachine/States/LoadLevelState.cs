using Common.Infrastructure.Factories.CharacterFactory;
using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Services.Progress;
using Common.Infrastructure.Services.SceneContext;
using Common.Infrastructure.Services.SceneLoading;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using Cysharp.Threading.Tasks;

namespace Common.Infrastructure.StateMachine.States
{
    public class LoadLevelState : State, IState
    {
        private readonly ISceneContextService _sceneContextService;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICharacterFactory _characterFactory;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(ISceneContextService sceneContextService, IStaticDataService staticDataService, 
            IPersistentProgressService progressService, ISceneLoader sceneLoader, ICharacterFactory characterFactory,
            IUIFactory uiFactory)
        {
            _sceneContextService = sceneContextService;
            _staticDataService = staticDataService;
            _progressService = progressService;
            _sceneLoader = sceneLoader;
            _characterFactory = characterFactory;
            _uiFactory = uiFactory;
        }

        public void Enter() => 
            _sceneLoader.LoadSceneAsync(Constants.Scenes.GameScene, OnGameSceneLoaded).Forget();
        public override void Exit() => _uiFactory.HideLoadingCurtain();
        private void OnGameSceneLoaded()
        {
            SpawnPlayer();

            StateMachine.Enter<GameLoopState>();
        }
        private void SpawnPlayer()
        {
            var spawnPoint = _sceneContextService.CharacterSpawnPoint;
            var skinIndex = _progressService.SaveData.Progress.SelectedCharacterSkinIndex;
            CharacterStaticData characterData = skinIndex.HasValue ? 
                _staticDataService.GameStaticData.GetCharacterStaticData((int)skinIndex) : 
                _staticDataService.GameStaticData.GetDefaultCharacterStaticData();

            _characterFactory.InstantiateCharacter(characterData.Prefab, spawnPoint);
        }
    }
}