using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "SaveLoad_ShootsHits",
        menuName = "Configs/SaveLoad")]
    public sealed class SaveLoad_ShotsHits : ScriptableObject
    {
        public int? Shots { get; set; } = null;

        public int? Hits { get; set; } = null;

        private const string _shotsKey = "shoots";
        private const string _hitsKey = "hits";

        public void Save()
        {
            if (!(Shots.HasValue && Hits.HasValue))
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

            PlayerPrefs.SetInt(_shotsKey, Shots.Value);
            PlayerPrefs.SetInt(_hitsKey, Hits.Value);
        }

        public bool Load()
        {
            if (HasSavedData())
            {
                Shots = PlayerPrefs.GetInt(_shotsKey);
                Hits = PlayerPrefs.GetInt(_hitsKey);

                return true;
            }

            return false;
        }

        private bool HasSavedData()
        {
            return PlayerPrefs.HasKey(_shotsKey) && PlayerPrefs.HasKey(_hitsKey);
        }
    }
}