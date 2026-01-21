using System;
using System.Collections.Generic;
using System.Linq;

namespace Events
{
    public static class EventsSelector
    {
        public static IEnumerable<GameEvent> GetByType(IEnumerable<GameEvent> events, EventType type)
        {
            return events
                .Where(e => e.Type == type);
        }

        public static int GetPickedItemsCount(IEnumerable<GameEvent> events)
        {
            return events.Count(e => e.Type == EventType.ItemPicked);
        }

        public static IEnumerable<GameEvent> GetMostFrequently(IEnumerable<GameEvent> events, out EventType frequencyType)
        {
            int count = 0;
            frequencyType = 0;

            foreach (EventType type in Enum.GetValues(typeof(EventType)))
            {
                var currentCount = events.Count(e => e.Type == type);

                if (currentCount > count)
                {
                    count = currentCount;
                    frequencyType = type;
                }
            }

            return GetByType(events, frequencyType);
        }

        public static IEnumerable<GameEvent> GetLastEvents(IEnumerable<GameEvent> events, int N)
        {
            return events
                .OrderBy(e => e.Time)
                .Take(N);
        }
    }
}