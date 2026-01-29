using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace NewsFeed
{
    public class NewsFeedHelper : MonoBehaviour
    {
        [SerializeField]
        private NewsFeedView _view;

        [SerializeField]
        private ErrorsView _errorsView;

        [SerializeField]
        private NewsLoaderConfig _newsLoaderConfig;

        [SerializeField]
        private AdditionLoadConfig _additionLoadConfig;

        private NewsLoader _loader;

        private AdditionLoader _additionLoader;

        private CancellationTokenSource _cts = new();

        private int _mockDelay = 100;

        private void Awake()
        {
            _additionLoader = new AdditionLoader(_additionLoadConfig);

            _loader = new NewsLoader(_additionLoader, _newsLoaderConfig);
        }

        private void OnEnable()
        {
            _loader.OnLoadError += ShowLoadError;
        }

        private void OnDisable()
        {
            _loader.OnLoadError -= ShowLoadError;
            _cts.Cancel();
        }

        private async void Start()
        {
            var news = await _loader.LoadNewsAsync(_cts);

            _view.ShowNews(news);

            while (!_cts.IsCancellationRequested)
            {
                _view.IsWaiting = true;

                var newsAdd = await _loader.LoadNewsAsync(_cts);

                _view.IsWaiting = false;

                while (_view.IsShowing)
                {
                    await Task.Delay(_mockDelay);
                }

                _view.ShowNews(newsAdd);
            }
        }

        private void ShowLoadError(string message)
        {
            _errorsView.ShowText($"{message}\n");
        }
    }
}