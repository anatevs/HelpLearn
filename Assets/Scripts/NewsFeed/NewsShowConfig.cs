using System;
using UnityEngine;
using System.Collections.Generic;

namespace NewsFeed
{
    [CreateAssetMenu(
        fileName = "NewsShowConfig",
        menuName = "Configs/News")]
    public class NewsShowConfig : ScriptableObject
    {
        public float ShowDelay => _showDelay;

        public string ErrorTitle => _errorTitle;

        public ShowStringData TitleParams => _titleParams;
        public ShowStringData ContentParams => _contentParams;
        public ShowStringData DataParams => _dataParams;

        [SerializeField]
        private float _showDelay = 2f;

        [SerializeField]
        private string _dateFormat = "dd.mm.yyyy";

        [SerializeField]
        private string _errorTitle = "Errors at json-file data:";

        [SerializeField]
        private ShowStringData _errorParams;

        [SerializeField]
        private ShowStringData _titleParams;

        [SerializeField]
        private ShowStringData _contentParams;

        [SerializeField]
        private ShowStringData _dataParams;


        public string GetErrosMessage(List<string> errors)
        {
            if (errors.Count == 0)
            {
                return "";
            }

            var message = GetPartString(_errorTitle, _errorParams);
            foreach (var error in errors)
            {
                message = $"{message}{GetPartString(error, _errorParams)}\n";
            }

            return message;
        }

        public string GetNewsString(NewsItem item)
        {
            return $"{GetPartString(item.TimeStamp.ToString(_dateFormat), _dataParams)}" +
                $"{GetPartString(item.Title, _titleParams)}" +
                $"{GetPartString(item.Content, _contentParams)}";
        }

        private string GetPartString(string part, ShowStringData showParam)
        {
            return $"<color=#{showParam.ColorHEX}>{part}</color>{showParam.EndString}";
        }
    }
    [Serializable]
    public struct ShowStringData
    {
        public string EndString;
        public string ColorHEX => ColorUtility.ToHtmlStringRGBA(_color);

        [SerializeField]
        private Color _color;
    }
}