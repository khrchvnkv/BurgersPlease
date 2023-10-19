using UnityEngine;

namespace Common.Infrastructure.Services.SceneContext
{
    public sealed class SceneContextService : ISceneContextService
    {
        public Transform CharacterSpawnPoint { get; set; }
    }
}