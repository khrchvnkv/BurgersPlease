using Common.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.UnityLogic
{
    public class GameRunner : MonoBehaviour
    {
        private void Awake()
        {
            // Start from any scene

            var currentScene = SceneManager.GetActiveScene().name;
            const string bootstrapScene = Constants.Scenes.BootstrapScene;

            if (currentScene != bootstrapScene)
            {
                var bootstrapper = FindObjectOfType<GameBootstrapper>();
                if (bootstrapper is null)
                {
                    SceneManager.LoadScene(bootstrapScene);
                }
            }
        }
    }
}