using GameManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class GameplayController : MonoBehaviour,
        IResetable
    {
        public event Action<IGameModifier> OnModifierAdded;
        public event Action<IGameModifier> OnModifierRemoved;
        public event Action<IGameModifier> OnModifierCanceled;

        public IReadOnlyList<IGameModifier> Modifiers => _gameModifiers;

        [SerializeField]
        private GameModifiersConfig _gameModifiersConfig;

        private GameModifierConfig[] _initConfigs;

        private ModifiersSpawner _spawner;

        private readonly List<IGameModifier> _gameModifiers = new();

        public void Init(ModifiersSpawner spawner)
        {
            _spawner = spawner;

            _initConfigs = _gameModifiersConfig.Configs;

            InitLevelModifiers();
        }

        public void ResetLevel()
        {
            for (int i = _gameModifiers.Count - 1; i >= 0; i--)
            {
                RemoveModifier(_gameModifiers[i]);
            }

            InitLevelModifiers();
        }

        private void InitLevelModifiers()
        {
            for (int i = 0; i < _initConfigs.Length; i++)
            {
                AddModifier(_initConfigs[i]);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _gameModifiers.Count; i++)
            {
                _gameModifiers[i].OnExitGameplay();
            }
        }

        private void Update()
        {
            for (int i = _gameModifiers.Count - 1; i >= 0; i--)
            {
                _gameModifiers[i].Tick(Time.deltaTime);
            }
        }

        public void AddModifier(IGameModifier modifier)
        {
            _gameModifiers.Add(modifier);
            modifier.OnEnterGameplay();
            OnModifierAdded?.Invoke(modifier);
        }

        public void AddModifier(GameModifierConfig config)
        {
            var modifier = _spawner.Create(config);

            AddModifier(modifier);
        }

        public void RemoveModifier(IGameModifier modifier)
        {
            _gameModifiers.Remove(modifier);
            OnModifierRemoved?.Invoke(modifier);
        }

        public void CancelModifier(IGameModifier modifier)
        {
            modifier.OnExitGameplay();
            RemoveModifier(modifier);
            OnModifierCanceled?.Invoke(modifier);
        }
    }
}