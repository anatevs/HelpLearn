using UnityEngine;

namespace Gameplay
{
    public class BarrelsSevice : MonoBehaviour
    {
        [SerializeField]
        private Barrel _prefab;

        private Vector3[] _positions;

        private Barrel[] _barrels;

        private void Awake()
        {
            _barrels = GetComponentsInChildren<Barrel>();

            _positions = new Vector3[_barrels.Length];

            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i] = _barrels[i].transform.position;
            }
        }

        public void ResetLevel()
        {
            for (int i = 0; i < _barrels.Length; i++)
            {
                if (_barrels[i] != null)
                {
                    Unspwn(_barrels[i]);
                }
            }

            SpawnAll();
        }

        private void SpawnAll()
        {
            _barrels = new Barrel[_positions.Length];

            for (int i = 0; i < _positions.Length; i++)
            {
                var barrel = Instantiate(_prefab, transform);

                barrel.transform.position = _positions[i];

                _barrels[i] = barrel;
            }
        }

        private void Unspwn(Barrel barrel)
        {
            Destroy(barrel.gameObject);
        }
    }
}