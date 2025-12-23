using UnityEngine;

namespace GameManagement
{
    public class SaveLoad_WeaponParams : ScriptableObject
    {
        public float? Cooldown { get; set; } = null;

        public float? Speed { get; set; } = null;

        private const string _cooldownKey = "weaponCooldown";
        private const string _speedKey = "weaponSpeed";

        public void Save()
        {
            if (!(Cooldown.HasValue && Speed.HasValue))
            {
                Debug.Log("No value for shoots or hits to save");
                return;
            }

            PlayerPrefs.SetFloat(_cooldownKey, Speed.Value);
            PlayerPrefs.SetFloat(_speedKey, Speed.Value);

            Debug.Log("Current hits saved");
        }

        public bool Load()
        {
            if (HasSavedData())
            {
                Cooldown = PlayerPrefs.GetFloat(_cooldownKey);
                Speed = PlayerPrefs.GetFloat(_speedKey);

                return true;
            }

            return false;
        }

        private bool HasSavedData()
        {
            return PlayerPrefs.HasKey(_cooldownKey) && PlayerPrefs.HasKey(_speedKey);
        }
    }
}