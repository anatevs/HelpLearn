using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "SaveLoad_ShootsHits",
        menuName = "Configs/SaveLoad")]
    public class SaveLoad_ShootsHits : ScriptableObject
    {
        public int? Shoots { get; set; } = null;

        public int? Hits { get; set; } = null;

        private const string _shootsKey = "shoots";
        private const string _hitsKey = "hits";

        public void Save()
        {
            if (!(Shoots.HasValue && Hits.HasValue))
            {
                Debug.Log("No value for shoots or hits to save");
                return;
            }

            if (HasSavedData())
            {
                var prevHits = PlayerPrefs.GetInt(_hitsKey);

                if (prevHits > Hits.Value)
                {
                    Debug.Log("current record was not saved because previous record was bigger");
                    return;
                }
            }

            PlayerPrefs.SetInt(_shootsKey, Shoots.Value);
            PlayerPrefs.SetInt(_hitsKey, Hits.Value);
        }

        public bool Load()
        {
            if (HasSavedData())
            {
                Shoots = PlayerPrefs.GetInt(_shootsKey);
                Hits = PlayerPrefs.GetInt(_hitsKey);

                return true;
            }

            return false;
        }

        private bool HasSavedData()
        {
            return PlayerPrefs.HasKey(_shootsKey) && PlayerPrefs.HasKey(_hitsKey);
        }
    }
}