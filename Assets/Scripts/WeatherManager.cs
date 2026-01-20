using Events;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class WeatherManager : MonoBehaviour
    {
        [SerializeField]
        private CloudsConfig _cloudsConfig;

        private WeatherType _currentWeather;

        private Coroutine _currentCoroutine;

        private GameObject _clouds;

        private void Awake()
        {
            _cloudsConfig.Init();

            _clouds = Instantiate(_cloudsConfig.CloudsPrefab, transform);
            _clouds.SetActive(false);

            _cloudsConfig.SetToStart(_clouds);
        }


        private void Start()
        {
            //var e = new GameEvent(Events.EventType.WeatherChanged, Time.time, "test weather");

            StartCoroutine(RandomWeather());
        }

        private IEnumerator RandomWeather()
        {
            var nextTime = UnityEngine.Random.Range(3f, 10f);

            var nextWeather = UnityEngine.Random.Range(0, Enum.GetValues(typeof(WeatherType)).Length);

            Debug.Log($"{nextTime}, {nextWeather}");

            yield return new WaitForSeconds(nextTime);

            if (!TryChangeWeather((WeatherType)nextWeather))
            {
                StartCoroutine(RandomWeather());
            }
        }

        private bool TryChangeWeather(WeatherType newWeather)
        {
            if (_currentWeather == newWeather || newWeather == WeatherType.Normal)
            {
                return false;
            }
            else if (_currentCoroutine != null)
            {
                Debug.LogWarning($"Try to set new weather while {_currentWeather} is in progress");
                return false;
            }

            MakeWeather(newWeather);
            return true;
        }

        private void MakeWeather(WeatherType type)
        {
            _currentWeather = type;

            var e = new WeatherEvent(DateTime.Now, $"weather changed to {type}", type);

            GameSingleton.Instance.EventManager.TriggerEvent(e);

            if (type == WeatherType.Normal)
            {
                StartCoroutine(RandomWeather());
                return;
            }

            if (type == WeatherType.Storm)
            {
                StartCoroutine(MakeStorm());
            }
        }

        private IEnumerator MakeStorm()
        {
            _cloudsConfig.SetToStart(_clouds);
            _clouds.SetActive(true);

            var t = 0f;

            while(t <= _cloudsConfig.Duration)
            {
                t += Time.deltaTime;

                _cloudsConfig.FrameMove(_clouds);

                yield return null;
            }

            _clouds.SetActive(false);

            MakeWeather(WeatherType.Normal);

            _currentCoroutine = null;
        }
    }
}