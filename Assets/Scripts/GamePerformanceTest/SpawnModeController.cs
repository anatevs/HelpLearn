using Gameplay;
using System;
using UnityEngine;

namespace GameManagement
{
    public class SpawnModeController : MonoBehaviour
    {
        public event Action<IPrewarmPool> OnPrewarmPoolCreated;

        public GameSpawnMode Mode => _mode;

        [SerializeField]
        private GameSpawnMode _mode = GameSpawnMode.Naive;

        public IPool<T> CreatePool<T>(T prefab, int initSize, Transform objectsTransform) where T : MonoBehaviour, IPoolable
        {
            IPool<T> pool = null;

            if (_mode == GameSpawnMode.Naive)
            {
                pool = new MockPool<T>(prefab, objectsTransform);
            }
            else if (_mode == GameSpawnMode.Pool)
            {
                pool = new Pool<T>(prefab, initSize, objectsTransform);

                OnPrewarmPoolCreated?.Invoke(pool as IPrewarmPool);
            }

            return pool;
        }
    }
}