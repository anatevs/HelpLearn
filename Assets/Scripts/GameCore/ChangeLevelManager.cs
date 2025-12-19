using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCore
{
    public class ChangeLevelManager : MonoBehaviour
    {
        [SerializeField]
        private Button _toMenuButton;

        private int _startSceneIndex = 0;

        private void OnEnable()
        {
            _toMenuButton.onClick.AddListener(SetStartScene);
        }

        private void OnDisable()
        {
            _toMenuButton.onClick.RemoveListener(SetStartScene);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SetStartScene();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadScene();
            }
        }

        private void SetStartScene()
        {
            SceneManager.LoadScene(_startSceneIndex);
        }

        private void ReloadScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}