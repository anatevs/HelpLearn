using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CloudsConfig",
        menuName = "Configs/Clouds")]
    public class CloudsConfig : ScriptableObject
    {
        public GameObject CloudsPrefab => _cloudsPrefab;

        public float Duration => _duration;

        [SerializeField]
        private float[] _xRange = { 0f, -86f };

        [SerializeField]
        private float _duration = 10f;

        [SerializeField]
        private GameObject _cloudsPrefab;

        private float _speed = 1f;

        private Vector3 _xDirection = Vector3.right;

        public void Init()
        {
            var moveX = _xRange[1] - _xRange[0];

            _speed = moveX / _duration;

            _xDirection = Vector3.right * _speed;
        }

        public void SetToStart(GameObject cloudsGO)
        {
            cloudsGO.transform.position = Vector3.right * _xRange[0];
        }

        public void FrameMove(GameObject cloudsGO)
        {
            cloudsGO.transform.Translate(_xDirection * Time.deltaTime);
        }
    }
}