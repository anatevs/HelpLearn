using Gameplay;
using System;
using UnityEngine;

namespace UI
{
    public sealed class LoggerService :
        ILoggerService
    {
        public LogMessagesConfig LogMessagesConfig => _messageConfig;

        private readonly LogMessagesConfig _messageConfig;

        private readonly IHealth _playerHealth;
        private readonly ICollectService _collectService;

        public LoggerService(LogMessagesConfig logMessagesConfig,
            IHealth playerHealth,
            ICollectService collectService)
        {
            _messageConfig = logMessagesConfig;
            _playerHealth = playerHealth;
            _collectService = collectService;

            _playerHealth.OnHPChanged += LogHPChanged;
            _playerHealth.OnKilled += LogKilled;
            _collectService.OnChanged += LogItemPicked;
        }

        void IDisposable.Dispose()
        {
            _playerHealth.OnHPChanged -= LogHPChanged;
            _playerHealth.OnKilled -= LogKilled;
            _collectService.OnChanged -= LogItemPicked;
        }

        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogHPChanged(int newHP)
        {
            var message = string.Format(_messageConfig.HPChanged, newHP);
            Log(message);
        }

        public void LogSpawnItem(string name)
        {
            LogOneParamMessage(name, _messageConfig.ItemSpawed);
        }

        public void LogItemPicked(string name, int newValue)
        {
            var message = string.Format(_messageConfig.ItemPicked, name, newValue);
            Log(message);
        }

        public void LogKilled()
        {
            var message = $"{_messageConfig.PlayerKilled}";
            Log(message);
        }

        public void LogInputSwitched(string name)
        {
            LogOneParamMessage(name, _messageConfig.InputSwitched);
        }

        public void LogModifierApply(string name)
        {
            LogOneParamMessage(name, _messageConfig.ModifierApply);
        }

        public void LogModifierCancel(string name)
        {
            LogOneParamMessage(name, _messageConfig.ModifierCancel);
        }

        private void LogOneParamMessage(string param, string formatString)
        {
            var message = string.Format(formatString, param);
            Log(message);
        }
    }
}