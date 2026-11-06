using UnityEngine.SceneManagement;

namespace GameManagement
{
    public sealed class SceneLoader
    {
        public void LoadGameScene()
        {
            var currentIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentIndex == 0)
            {
                SceneManager.LoadSceneAsync(currentIndex + 1);
            }
        }
    }
}