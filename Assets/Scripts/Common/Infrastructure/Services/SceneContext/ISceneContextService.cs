using UnityEngine;

namespace Common.Infrastructure.Services.SceneContext
{
    public interface ISceneContextService
    {
        Transform CharacterSpawnPoint { get; set; }
    }
}