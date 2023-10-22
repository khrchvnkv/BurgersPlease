using Common.Infrastructure.Services.AssetsManagement;
using Common.Infrastructure.Services.Progress;
using Common.StaticData;

namespace Common.Infrastructure.Services.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPersistentProgressService _progressService;
        
        public GameStaticData GameStaticData { get; private set; }

        public StaticDataService(IAssetProvider assetProvider, IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _progressService = progressService;
        }
        public void Load()
        {
            GameStaticData = _assetProvider.LoadGameStaticData();
        }
        public CharacterStaticData GetCharacterStaticData()
        {
            var skinIndex = _progressService.SaveData.Progress.SelectedCharacterSkinIndex;
            return skinIndex.HasValue ? 
                GameStaticData.GetCharacterStaticData(skinIndex) : 
                GameStaticData.GetDefaultCharacterStaticData();
        }
    }
}