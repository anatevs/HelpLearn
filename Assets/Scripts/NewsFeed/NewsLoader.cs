using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading;

namespace NewsFeed
{
    public class NewsLoader
    {
        public event Action<string> OnLoadError;

        public List<string> JsonItemsErrors => _errorsLog;

        private readonly NewsLoaderConfig _config;

        private string _streamingPath;

        private JsonSerializerSettings _settings = new();

        private readonly List<string> _errorsLog = new();

        private bool _isFirstLoad = true;

        private readonly AdditionLoader _additionLoader;

        private NewsItem _endNewsItem;

        public NewsLoader(AdditionLoader additionLoader, NewsLoaderConfig config)
        {
            _additionLoader = additionLoader;
            _config = config;

            _streamingPath = Path.Combine(Application.streamingAssetsPath, _config.Filename);

            _settings = new JsonSerializerSettings()
            {
                Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    if (args.CurrentObject == args.ErrorContext.OriginalObject)
                    {
                        HandleError(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    }
                },
                DateFormatString = _config.DateFormat
            };

            _endNewsItem = new NewsItem(_config.EndNews, null, DateTime.Now);
        }

        public async Task<List<NewsItem>> LoadNewsAsync(CancellationTokenSource cts)
        {
            var result = new List<NewsItem>();

            if (!cts.IsCancellationRequested)
            {
                try
                {
                    var fileString = await ReadJsonFileAsync(cts);

                    result = JsonConvert.DeserializeObject<List<NewsItem>>(fileString, _settings);
                }
                catch (FileNotFoundException)
                {
                    HandleError(_config.NoFileError);
                }
                catch (FileLoadException exception)
                {
                    HandleError($"{_config.LoadError}{exception}");
                }
                catch (Exception exception)
                {
                    if (!cts.IsCancellationRequested)
                    {
                        HandleError($"{_config.OtherError}{exception}");
                    }
                    else
                    {
                        result.Add(_endNewsItem);
                    }
                }
            }
            return result;
        }

        private async Task<string> ReadJsonFileAsync(CancellationTokenSource cts)
        {
            if (_isFirstLoad)
            {
                _isFirstLoad = false;
                return await File.ReadAllTextAsync(_streamingPath);
            }
            else
            {
                return await _additionLoader.LoadAsync(cts);
            }
        }

        private void HandleError(string error)
        {
            _errorsLog.Add(error);
            OnLoadError?.Invoke(error);
        }
    }
}