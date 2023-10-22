using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Ecs.Providers.Character;
using Leopotam.Ecs;
using Zenject;

namespace Common.UnityLogic.Ecs.Systems.InteractiveZones
{
    public class ItemsStackingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTagComponent, PlayerStackComponent, StackingComponent>.Exclude<BlockDurationComponent> _stackingPlayerFilter = null;
        private readonly EcsFilter<PlayerTagComponent, PlayerStackComponent, UnstackingComponent>.Exclude<BlockDurationComponent> _unstackingPlayerFilter = null;

        private float _timeBtwInteractivity;
        
        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _timeBtwInteractivity = staticDataService.GameStaticData.StackingStaticData.TimeBtwInteractivity;
        }
        public void Run()
        {
            Stacking();
            Unstacking();
        }
        private void Stacking()
        {
            foreach (var i in _stackingPlayerFilter)
            {
                ref var stackable = ref _stackingPlayerFilter.Get2(i);
                if (stackable.CurrentStackValue >= stackable.MaxStackValue) continue;
                
                stackable.CurrentStackValue++;
                UnityEngine.Debug.Log($"TTR: Stack {stackable.CurrentStackValue}/{stackable.MaxStackValue}");
                if (stackable.CurrentStackValue < stackable.MaxStackValue)
                {
                    ref var entity = ref _stackingPlayerFilter.GetEntity(i);
                    entity.Get<BlockDurationComponent>() = new BlockDurationComponent(timer: _timeBtwInteractivity);
                }
            }
        }
        private void Unstacking()
        {
            foreach (var i in _unstackingPlayerFilter)
            {
                ref var stackable = ref _unstackingPlayerFilter.Get2(i);
                if (stackable.CurrentStackValue <= 0) continue;
                
                stackable.CurrentStackValue--;
                UnityEngine.Debug.Log($"TTR: Unstack {stackable.CurrentStackValue}/{stackable.MaxStackValue}");
                if (stackable.CurrentStackValue > 0)
                {
                    ref var entity = ref _unstackingPlayerFilter.GetEntity(i);
                    entity.Get<BlockDurationComponent>() = new BlockDurationComponent(timer: _timeBtwInteractivity);
                }
            }
        }
    }
}