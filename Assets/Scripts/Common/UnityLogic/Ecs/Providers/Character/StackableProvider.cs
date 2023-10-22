using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Voody.UniLeo;
using Zenject;

namespace Common.UnityLogic.Ecs.Providers.Character
{
    public class StackableProvider : MonoProvider<PlayerStackComponent>
    {
        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            var characterStaticData = staticDataService.GetCharacterStaticData();
            value.CurrentStackValue = 0;
            value.MaxStackValue = characterStaticData.Data.DefaultStackCount;
        }
    }
}