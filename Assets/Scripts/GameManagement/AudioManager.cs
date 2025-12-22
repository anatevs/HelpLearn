using UnityEngine;

namespace GameManagement
{
    public sealed class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _fire;

        [SerializeField]
        private AudioClip _targetDestroy;

        public void PlayFire()
        {
            PlayClip(_fire);
        }

        public void PlayTargetDestroy()
        {
            PlayClip(_targetDestroy);
        }

        private void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}