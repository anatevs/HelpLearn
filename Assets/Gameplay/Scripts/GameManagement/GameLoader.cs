using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public sealed class GameLoader : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}