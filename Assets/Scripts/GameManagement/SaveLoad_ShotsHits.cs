using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "SaveLoad_ShootsHits",
        menuName = "Configs/SaveLoad/Shots")]
    public class SaveLoad_ShotsHits : SaveLoadSO<ShotsData>
    {
        public int Shots
        {
            get => _currentData.Shots;
            set => _currentData.Shots = value;
        }

        public int Hits
        {
            get => _currentData.Hits;
            set => _currentData.Hits = value;
        }

        public override void Save()
        {
            if (HasSavedData())
            {
                if (_savedData.Hits > Hits)
                {
                    Debug.Log("current record was not saved because previous record was bigger");
                    return;
                }
            }

            base.Save();
        }

        public override bool Load()
        {
            return base.Load();
        }

        protected override bool HasDataToSave()
        {
            return _currentData.Hits != 0;
        }
    }
}