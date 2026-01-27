using UnityEngine;

namespace NewsFeed
{
    public class NewsFeedHelper : MonoBehaviour
    {
        [SerializeField]
        private NewsFeedView _view;

        private readonly NewsLoader _loader = new();

        private void OnEnable()
        {
            _loader.OnLoadError += ShowLoadError;
        }

        private void OnDisable()
        {
            _loader.OnLoadError -= ShowLoadError;
        }

        private async void Start()
        {
            var res = await _loader.LoadNewsAsync();

            _view.ShowNewsAndErrors(res, _loader.JsonItemsErrors);
        }

        private void ShowLoadError(string message)
        {
            _view.ShowString($"{message}\n");
        }
    }
}