using UnityEngine;

namespace GameManagement
{
    public class SaveLoadSO<T> : ScriptableObject where T : struct
    {
        private readonly string _saveKey = typeof(T).Name;

        protected T _savedData;

        protected T _currentData;

        public virtual void Save()
        {
            if (!HasDataToSave())
            {
                Debug.Log($"Nothing to save for {_savedData}");
                return;
            }

            var jsonData = JsonUtility.ToJson(_currentData);
            PlayerPrefs.SetString(_saveKey, jsonData);

            Debug.Log($"Current {_saveKey} saved");
        }

        public virtual void Load()
        {
            _savedData = new();
            _currentData = new();

            if (HasLoadedData())
            {
                var jsonData = PlayerPrefs.GetString(_saveKey);
                _savedData = JsonUtility.FromJson<T>(jsonData);

                _currentData = _savedData;

                return;
            }
        }

        public bool HasLoadedData()
        {
            return PlayerPrefs.HasKey(_saveKey);
        }

        protected virtual bool HasDataToSave()
        {
            return true;
        }
    }
}