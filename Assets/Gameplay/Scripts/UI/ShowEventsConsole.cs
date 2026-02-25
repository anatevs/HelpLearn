using EventBusNamespace;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class ShowEventsConsole
    {
        public void ShowAll()
        {
            Debug.Log("");
            Debug.Log("All events:");

            foreach (var e in EventBus.Events)
            {
                ShowOneEvent(e);
            }
        }

        public void ShowLastN(int n)
        {
            Debug.Log("");
            Debug.Log($"Last {n} events:");

            var lastN = EventBus.Events
                .Reverse()
                .Take(n);

            foreach (var e in lastN)
            {
                ShowOneEvent(e);
            }
        }

        private void ShowOneEvent(IGameEvent e)
        {
            var result = $"{e.Name}";

            if (e.Description != "")
            {
                result = $"{result}. {e.Description}";
            }

            Debug.Log(result);
        }
    }
}