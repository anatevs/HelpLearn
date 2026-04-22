using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class WavesManager : MonoBehaviour
    {
        public event Action<WaveCompletedInfo, bool> OnWaveCompleted;

        public event Action<float> OnWaitPortionChanged;

        public event Action OnWaveStarted;

        public event Action OnAllWavesCompleted;

        private Wave _currentWave;

        private readonly List<WaveCompletedInfo> _completedWaves = new();

        private EnemyWaveConfig[] _waveConfigs;
        private EnemySpawnService _enemySpawnService;
        private int _nextIndex = 0;
        private Coroutine _nextWaitCoroutine = null;
        private float _nextWaitTime = 0;

        public void Constuct(EnemySpawnService enemySpawnService)
        {
            _enemySpawnService = enemySpawnService;

            _waveConfigs = _enemySpawnService.Config.WaveConfigs;

            foreach (var waveConfig in _waveConfigs)
            {
                waveConfig.Init();
            }

            _enemySpawnService.OnEnemyKilled += AddKilledEnemy;
        }

        public void ResetLevel()
        {
            if (_nextWaitCoroutine != null)
            {
                StopCoroutine(_nextWaitCoroutine);
                _nextWaitCoroutine = null;
            }

            _completedWaves.Clear();
            _nextIndex = 0;

            if (_currentWave != null)
            {
                _currentWave.OnWaveEnded -= HandleWaveEnd;
            }

            StartNewWave();
        }

        private void OnDisable()
        {
            if (_currentWave != null)
            {
                _currentWave.OnWaveEnded -= HandleWaveEnd;
            }

            _enemySpawnService.OnEnemyKilled -= AddKilledEnemy;
        }

        public void StartNewWave()
        {
            OnWaveStarted?.Invoke();

            var config = _waveConfigs[_nextIndex];

            _currentWave = new Wave(_nextIndex + 1, config.GetWaveSize());

            _currentWave.OnWaveEnded += HandleWaveEnd;

            _enemySpawnService.SpawnEnemies(config);

            _nextIndex++;
        }

        public void AddKilledEnemy(Enemy enemy)
        {
            _currentWave.AddKilledEnemy(enemy.Config.KillReward);
        }

        private void HandleWaveEnd(WaveCompletedInfo waveInfo)
        {
            _currentWave.OnWaveEnded -= HandleWaveEnd;
            _currentWave = null;

            var areAllEnded = _nextIndex >= _waveConfigs.Length;

            OnWaveCompleted?.Invoke(waveInfo, areAllEnded);

            if (!areAllEnded)
            {
                _nextWaitCoroutine = StartCoroutine(NextWaiting());
            }
            else
            {
                OnAllWavesCompleted?.Invoke();
            }
        }

        private IEnumerator NextWaiting()
        {
            var waitTime = _waveConfigs[_nextIndex - 1].NextWaveDelay;
            while (_nextWaitTime < waitTime)
            {
                _nextWaitTime += Time.deltaTime;
                OnWaitPortionChanged?.Invoke(_nextWaitTime / waitTime);
                yield return null;
            }

            _nextWaitTime = 0;
            StartNewWave();
        }
    }
}