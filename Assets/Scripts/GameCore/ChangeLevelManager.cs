using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore
{
    public class ChangeLevelManager : MonoBehaviour
    {
        private int _startSceneIndex = 0;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SetStartScene();
            }
        }

        private void SetStartScene()
        {
            SceneManager.LoadScene(_startSceneIndex);
        }
    }
}