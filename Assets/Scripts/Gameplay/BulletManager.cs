using UnityEngine;

namespace Gameplay
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private Bullet _prefab;

        [SerializeField]
        private int _initPoolSize = 10;

        private Pool<Bullet> _pool;

        private void Awake()
        {
            _pool = new Pool<Bullet>(_prefab, _initPoolSize, transform);
        }

        public void Shoot(Transform shootPoint)
        {
            var bullet = _pool.Spawn(shootPoint);

            bullet.OnCollided += Pool;
        }

        private void Pool(Bullet bullet)
        {
            _pool.Unspawn(bullet);

            bullet.OnCollided -= Pool;
        }
    }
}