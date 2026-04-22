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
                    if (!_barrels[i].IsExploded)
                    {
                        continue;
                    }
                    else
                    {
                        Unspawn(_barrels[i]);
                    }
                }

                SpawnAtIndex(i);
            }
        }

        private void SpawnAtIndex(int index)
        {
            var barrel = Instantiate(_prefab, transform);

            barrel.transform.position = _positions[index];

            _barrels[index] = barrel;
        }

        private void Unspawn(Barrel barrel)
        {
            Destroy(barrel.gameObject);
        }
    }
}