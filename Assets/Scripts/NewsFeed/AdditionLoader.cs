using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace NewsFeed
{
    public class AdditionLoader
    {
        private readonly AdditionLoadConfig _config;

        private int _currentIndex = 0;

        public AdditionLoader(AdditionLoadConfig config)
        {
            _config = config;
        }

        public async Task<string> LoadAsync(CancellationTokenSource cts)
        {
            if (_currentIndex < _config.Filenames.Length)
            {
                await Task.Delay(_config.ResourcesDelay);

                var request = Resources.LoadAsync($"{_config.ResourcesPath}{_config.Filenames[_currentIndex]}");

                while (!request.isDone)
                {
                    await Task.Delay(_config.ReadDelay);
                }

                _currentIndex++;

                var text = request.asset as TextAsset;

                return text.text;
            }

            cts.Cancel();

            return null;
        }
    }
}