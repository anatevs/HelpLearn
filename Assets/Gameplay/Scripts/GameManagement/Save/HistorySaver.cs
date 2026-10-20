using EventBusNamespace;
using System;
using System.IO;
using UI;
using UnityEngine;

namespace GameManagement
{
    public class HistorySaver
    {
        private string _path;

        private const string _filename = "EventsLog.txt";

        private const string _sessionsDelimiter = "\n\n\n";

        public void Init()
        {
            _path = Path.Combine(Application.persistentDataPath, _filename);

            SetTitle();
        }

        public void SetTitle()
        {
            var title = $"Session from {DateTime.Now:dd.MM.yyyy, HH:mm}\n";

            if (File.Exists(_path))
            {
                title = $"{_sessionsDelimiter}{title}";
            }

            try
            {
                File.AppendAllText(_path, title);
            }
            catch (Exception e)
            {
                Debug.Log($"Error at writing event log file: {e}");
            }
        }

        public void WriteEvent(IGameEvent e)
        {
            File.AppendAllText(_path, $"{EventUIInfo.GetEventString(e)}\n");
        }
    }
}