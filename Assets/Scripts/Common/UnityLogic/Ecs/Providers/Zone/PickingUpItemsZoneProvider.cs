using Common.Infrastructure.Services.ECS;
using Common.UnityLogic.Ecs.Components.Zones;
using Common.UnityLogic.Ecs.Events;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Zone
{
    public class PickingUpItemsZoneProvider : PlayerTriggerZoneProvider<PickingUpItemsZoneComponent>
    {
        private IEcsStartup _ecsStartup;

        [Inject]
        private void Construct(IEcsStartup startup)
        {
            _ecsStartup = startup;
        }
        protected override void OnPlayerEntered()
        {
            base.OnPlayerEntered();
            _ecsStartup.SendMessage(new PickingUpItemsZoneEnteredEvent());
        }
        protected override void OnPlayerExited()
        {
            base.OnPlayerExited();
            _ecsStartup.SendMessage(new PickingUpItemsZoneExitedEvent());
        }
    }
}