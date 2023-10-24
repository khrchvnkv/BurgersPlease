using Common.Infrastructure.Factories.ItemFactory;
using Common.Infrastructure.Services.Progress;
using Common.Infrastructure.Services.SaveLoad;
using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Ecs.Components.Characters;
using Common.UnityLogic.Ecs.Components.Stacking;
using Common.UnityLogic.Ecs.Components.Zones;
using Leopotam.Ecs;
using Zenject;

namespace Common.UnityLogic.Ecs.Systems.Zones
{
    public class SalesZoneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SalesZoneTagComponent, StackingComponent> _salesZoneFilter = null;
        private readonly EcsFilter<PlayerTagComponent, StackDataComponent>.Exclude<BlockUnstackDurationComponent> _playerFilter = null;

        private IItemFactory _itemFactory;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        private ulong _salePrice;
        private float _stackingBlockTimer;
        
        [Inject]
        private void Construct(IItemFactory itemFactory, IPersistentProgressService progressService, 
            ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _itemFactory = itemFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _stackingBlockTimer = staticDataService.GameStaticData.StackingStaticData.StackingTimer;
            _salePrice = staticDataService.GameStaticData.ZonesStaticData.SalePrice;
        }

        public void Run()
        {
            foreach (var i in _salesZoneFilter)
            {
                ref var saleZoneStack = ref _salesZoneFilter.Get2(i);
                foreach (var j in _playerFilter)
                {
                    ref var playerStack = ref _playerFilter.Get2(j);
                    if (playerStack.IsEmpty || 
                        !playerStack.CanUnstackToStack(ref saleZoneStack)) continue;
                    
                    _itemFactory.DespawnStackableCreationItem(ref playerStack);
                    playerStack.UpdateMaxCollectedLogo();
                    playerStack.UpdateStackItemType();
                    var playerEntity = _playerFilter.GetEntity(j);
                    playerEntity.Get<BlockUnstackDurationComponent>() = new BlockUnstackDurationComponent(_stackingBlockTimer);
                    GetReward();
                }
            }
        }
        private void GetReward()
        {
            _progressService.SaveData.Progress.AddCurrency(_salePrice);
            _saveLoadService.SaveData();
        }
    }
}