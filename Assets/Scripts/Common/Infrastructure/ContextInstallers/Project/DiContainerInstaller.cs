using Common.Infrastructure.Factories.CharacterFactory;
using Common.Infrastructure.Factories.ItemFactory;
using Common.Infrastructure.Factories.UIFactory;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.DontDestroyOnLoadCreator;
using Common.Infrastructure.Services.ECS;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.Progress;
using Common.Infrastructure.Services.SaveLoad;
using Common.Infrastructure.Services.SceneContext;
using Common.Infrastructure.Services.SceneLoading;
using Common.Infrastructure.Services.StaticData;
using Common.Infrastructure.Services.UpdateSystem;
using Common.Infrastructure.StateMachine;
using Common.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.ContextInstallers.Project
{
    public sealed class DiContainerInstaller : MonoInstaller
    {
        [SerializeField] private EcsStartup _ecsStartup;
        [SerializeField] private DontDestroyOnLoadCreator _dontDestroyOnLoadCreator;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private UpdateSystem _updateSystem;

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindServices();
            BindMonoServices();
            BindFactories();
        }
        private void BindMonoServices()
        {
            Container.Bind<IEcsStartup>().FromInstance(_ecsStartup).AsSingle();
            Container.Bind<IDontDestroyOnLoadCreator>().FromInstance(_dontDestroyOnLoadCreator).AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
            Container.Bind<IUpdateSystem>().FromInstance(_updateSystem).AsSingle();
        }
        private void BindServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().FromNew().AsSingle();
            Container.Bind<ISceneContextService>().To<SceneContextService>().FromNew().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().FromNew().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().FromNew().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();
            
#if UNITY_EDITOR
            Container.Bind<IInputService>().To<StandaloneInputService>().FromNew().AsSingle();
#else
            Container.Bind<IInputService>().To<MobileInputService>().FromNew().AsSingle();
#endif
        }
        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().FromNew().AsSingle();
            Container.Bind<BootstrapState>().FromNew().AsSingle();
            Container.Bind<LoadLevelState>().FromNew().AsSingle();
            Container.Bind<GameLoopState>().FromNew().AsSingle();
        }
        private void BindFactories()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().FromNew().AsSingle();
            Container.Bind<ICharacterFactory>().To<CharacterFactory>().FromNew().AsSingle();
            Container.Bind<IItemFactory>().To<ItemFactory>().FromNew().AsSingle();
            Container.Bind<IZenjectFactory>().To<ZenjectFactory>().AsSingle();
        }
    }
}
