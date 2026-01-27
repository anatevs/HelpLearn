using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

namespace NewsFeed
{
    public class NewsLoader
    {
        public event Action<string> OnLoadError;

        public List<string> JsonItemsErrors => _jsonErrors;

        private string _dateFormat = "yyyy-mm-dd";

        private string _filename = "news.json";

        private string _path;

        private string _noFileError = "json file not found";

        private string _loadError = "load json error: ";

        private string _otherError = "read json error: ";

        private JsonSerializerSettings _settings = new();

        private readonly List<string> _jsonErrors = new();

        public NewsLoader()
        {
            _path = Path.Combine(Application.streamingAssetsPath, _filename);

            _settings = new JsonSerializerSettings()
            {
                Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    if (args.CurrentObject == args.ErrorContext.OriginalObject)
                    {
                        _jsonErrors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    }
                },
                DateFormatString = _dateFormat
            };
        }

        public async Task<List<NewsItem>> LoadNewsAsync()
        {
            var result = new List<NewsItem>();
            try
            {
                var fileString = await File.ReadAllTextAsync(_path);

                result = JsonConvert.DeserializeObject<List<NewsItem>>(fileString, _settings);
            }
            catch (FileNotFoundException)
            {
                OnLoadError?.Invoke(_noFileError);
            }
            catch(FileLoadException exception)
            {
                OnLoadError?.Invoke($"{_loadError}{exception}");
            }
            catch(Exception exception)
            {
                OnLoadError?.Invoke($"{_otherError}{exception}");
            }

            return result;
        }
    }
}