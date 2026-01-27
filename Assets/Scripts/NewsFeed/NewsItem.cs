#nullable enable
using System;

namespace NewsFeed
{ 
    public class NewsItem
    {
        public string? Title => _title ?? "No title";
        public string? Content => _content ?? "No content";
        public DateTime TimeStamp => _timeStamp;

        private readonly string? _title;
        private readonly string? _content;
        private readonly DateTime _timeStamp;

        public NewsItem(string? title, string? content,
            DateTime timeStamp)
        {
            _title = title;
            _content = content;
            _timeStamp = timeStamp;
        }
    }
}