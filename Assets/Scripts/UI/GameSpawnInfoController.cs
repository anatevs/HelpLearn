using GameTest;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GameSpawnInfoController : MonoBehaviour
    {
        [SerializeField]
        private SpawnInfoView _viewPrefab;

        private SpawnCounterService _gameSpawnCounter;

        private readonly List<SpawnInfoController> _spawnInfoControllers = new();

        public void Init(SpawnCounterService gameSpawnCounter)
        {
            _gameSpawnCounter = gameSpawnCounter;

            _gameSpawnCounter.OnServiceAdded += HandleAddService;
        }

        private void HandleAddService(SpawnCounter counter, string objectsName)
        {
            var view = Instantiate(_viewPrefab, transform);

            view.SetObjectsName(objectsName);

            var spawnController = new SpawnInfoController(view, counter);

            _spawnInfoControllers.Add(spawnController);
        }

        public void OnDisable()
        {
            _gameSpawnCounter.OnServiceAdded -= HandleAddService;

            foreach (var controller in _spawnInfoControllers)
            {
                controller.Dispose();
            }
        }
    }
}