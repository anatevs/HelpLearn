using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "SoundsConfig",
        menuName = "Configs/Sounds")]
    public class SoundsConfig : ScriptableObject
    {
        [SerializeField]
        private SoundInfo[] _soundsInfo;

        private readonly Dictionary<SoundType, AudioClip> _sounds = new();

        public void Init()
        {
            _sounds.Clear();

            foreach (var soundInfo in _soundsInfo)
            {
                _sounds.Add(soundInfo.Type, soundInfo.Sound);
            }
        }

        public AudioClip GetSound(SoundType type)
        {
            if (!_sounds.TryGetValue(type, out AudioClip clip))
            {
                Debug.LogWarning($"there is no sound of type {type} assigned in sounds config");
            }

            return clip;
        }
    }

    [Serializable]
    public struct SoundInfo
    {
        public SoundType Type;
        public AudioClip Sound;
    }

    public enum SoundType
    {
        Shot = 0,
        Win = 1,
        Lose = 2,
        PickItem = 3,
        KillEnemy = 4
    }
}