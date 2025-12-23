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

        public virtual bool Load()
        {
            _savedData = new();
            _currentData = new();

            if (HasSavedData())
            {
                var jsonData = PlayerPrefs.GetString(_saveKey);
                _savedData = JsonUtility.FromJson<T>(jsonData);

                _currentData = _savedData;

                return true;
            }

            return false;
        }

        protected virtual bool HasDataToSave()
        {
            return true;
        }

        protected bool HasSavedData()
        {
            return PlayerPrefs.HasKey(_saveKey);
        }
    }
}