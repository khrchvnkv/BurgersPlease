using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Voody.UniLeo;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Character
{
    public class MovableProvider : MonoProvider<MovableComponent>
    {
        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            var staticData = staticDataService.GetCharacterStaticData();
            var defaultMovementSpeed = staticData.Data.DefaultMovementSpeed;
            value.Speed = defaultMovementSpeed;
        }
    }
}