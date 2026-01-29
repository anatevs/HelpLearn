using UnityEngine;

namespace NewsFeed
{
    [CreateAssetMenu(
        fileName = "NewsLoaderConfig",
        menuName = "Configs/NewsLoader")]
    public class NewsLoaderConfig : ScriptableObject
    {
        public string DateFormat => _dateFormat;
        public string Filename => _filename;
        public string NoFileError => _noFileError;
        public string LoadError => _loadError;
        public string OtherError => _otherError;
        public string EndNews => _endNews;

        [SerializeField]
        private string _dateFormat = "yyyy-mm-dd";
        [SerializeField]
        private string _filename = "news.json";
        [SerializeField]
        private string _noFileError = "json file not found";
        [SerializeField]
        private string _loadError = "load json error: ";
        [SerializeField]
        private string _otherError = "read json error: ";
        [SerializeField]
        private string _endNews = "No more news";
    }
}