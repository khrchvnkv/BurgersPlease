using Common.Infrastructure.Services.ECS;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.ContextInstallers.Scene
{
    public sealed class EcsInstaller : MonoInstaller
    {
        [SerializeField] private EcsStartup _ecsStartup;
        
        public override void InstallBindings() => 
            Container.Bind<IEcsStartup>().FromInstance(_ecsStartup).AsSingle().NonLazy();
    } 
}