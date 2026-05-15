using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        public event Action<string> OnModifierAdded;
        public event Action<string> OnModifierRemoved;

        public string[] Names => _gameModifiers.Select(x => x.Name).ToArray();

        private readonly List<IGameModifier> _gameModifiers = new();

        public void Init(IGameModifier[] gameModifiers)
        {
            foreach (var modifier in gameModifiers)
            {
                AddModifier(modifier);
            }
        }

        private void Start()
        {
            for (int i = 0; i < _gameModifiers.Count; i++)
            {
                _gameModifiers[i].OnEnterGameplay();
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
            OnModifierAdded?.Invoke(modifier.Name);
        }

        public void RemoveModifier(IGameModifier modifier)
        {
            if (_gameModifiers.Remove(modifier))
            {
                modifier.OnExitGameplay();
                OnModifierRemoved?.Invoke(modifier.Name);
            }
        }
    }
}