using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameManager
    {
        public bool IsPaused { get; set; }

        public void RestartGame()
        {
            var scene = SceneManager.GetActiveScene();

            SceneManager.LoadSceneAsync(scene.buildIndex);
        }
    }
}