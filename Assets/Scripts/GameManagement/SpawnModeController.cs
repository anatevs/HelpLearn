using Gameplay;
using UnityEngine;

namespace GameManagement
{
    public class SpawnModeController : MonoBehaviour
    {
        [SerializeField]
        private GameSpawnMode _mode = GameSpawnMode.Naive;

        public IPool<T> CreatePool<T>(T prefab, int initSize) where T : MonoBehaviour
        {
            IPool<T> pool = null;

            if (_mode == GameSpawnMode.Naive)
            {
                pool = new MockPool<T>(prefab);
            }
            else if (_mode == GameSpawnMode.Pool)
            {
                pool = new Pool<T>(prefab, initSize);
            }

            return pool;
        }
    }

    public enum GameSpawnMode
    {
        Naive = 0,
        Pool = 1
    }
}