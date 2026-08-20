using Gameplay;
using GameTest;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GameSpawnInfoPresenter : MonoBehaviour
    {
        [SerializeField]
        private SpawnInfoView _viewPrefab;

        private SpawnCounterService _gameSpawnCounter;

        private readonly List<ObjectSpawnInfoPresenter> _spawnInfoPresenters = new();

        public void Init(SpawnCounterService gameSpawnCounter)
        {
            _gameSpawnCounter = gameSpawnCounter;

            _gameSpawnCounter.OnServiceAdded += HandleAddService;
        }

        private void HandleAddService(SpawnCounter counter, string objectsName, List<IInfoPool> infoPools)
        {
            var view = Instantiate(_viewPrefab, transform);

            view.SetObjectsName(objectsName);

            var spawnPresenter = new ObjectSpawnInfoPresenter(view, counter, infoPools);

            _spawnInfoPresenters.Add(spawnPresenter);
        }

        public void OnDisable()
        {
            _gameSpawnCounter.OnServiceAdded -= HandleAddService;

            foreach (var presenter in _spawnInfoPresenters)
            {
                presenter.Dispose();
            }
        }
    }
}