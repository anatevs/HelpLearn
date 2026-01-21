using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    [CreateAssetMenu(fileName = "EventTextConfig", menuName = "Configs/EventText")]
    public class PrintEventsConfig : ScriptableObject
    {
        [SerializeField]
        private EventTextData[] _data = Enum.GetValues(typeof(EventType))
            .Cast<EventType>()
            .Select(c => new EventTextData(c, Color.yellow))
            .ToArray();

        private readonly Dictionary<EventType, string> _colors = new();

        public void Init()
        {
            _colors.Clear();

            foreach (var data in _data)
            {
                _colors[data.EventType] = $"#{ColorUtility.ToHtmlStringRGBA(data.Color)}";
            }
        }

        public string GetColorHex(EventType eventType)
        {
            return _colors[eventType];
        }
    }

    [Serializable]
    public struct EventTextData
    {
        public EventType EventType;

        public Color Color;

        public EventTextData(EventType eventType, Color color)
        {
            EventType = eventType;
            Color = color;
        }
    }
}