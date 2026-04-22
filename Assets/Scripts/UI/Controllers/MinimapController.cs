using Gameplay;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MinimapController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _playerIcon;

        [SerializeField]
        private Image _enemyIconPrefab;

        [SerializeField]
        private RectTransform _mapImage;

        [SerializeField]
        private Transform _playerReference;

        [SerializeField]
        private Transform _playerRotation;

        [SerializeField]
        private Transform[] _mapEdges;

        private EnemySpawnService _enemySpawnService;

        private Vector2 _mapDimentions;
        private Vector2 _areaDimentions;

        private readonly Dictionary<int, (Enemy enemy, Image icon)> _enemies = new();

        private Pool<Image> _enemyIconPool;

        private int _initPoolCount = 10;

        public void Construct(EnemySpawnService enemySpawnService)
        {
            _enemySpawnService = enemySpawnService;

            _enemySpawnService.OnEnemySpawned += SetNewEnemy;

            _enemySpawnService.OnEnemyKilled += UnlinkEnemy;

            _enemyIconPool = new Pool<Image>(_enemyIconPrefab, _initPoolCount, _playerIcon.parent);
        }

        public void ResetLevel()
        {
            foreach (var enemyIcon in _enemies.Values)
            {
                _enemyIconPool.Unspawn(enemyIcon.icon);
            }

            _enemies.Clear();
        }

        private void OnDisable()
        {
            if (_enemySpawnService != null)
            {
                _enemySpawnService.OnEnemySpawned -= SetNewEnemy;

                _enemySpawnService.OnEnemyKilled -= UnlinkEnemy;
            }
        }

        private void Start()
        {
            for (int i = 0; i < _mapEdges.Length; i++)
            {
                for (int j = i + 1; j < _mapEdges.Length; j++)
                {
                    if (_mapEdges[j].position.x < _mapEdges[i].position.x || _mapEdges[j].position.z < _mapEdges[i].position.z)
                    {
                        var temp = _mapEdges[j];
                        _mapEdges[j] = _mapEdges[i];
                        _mapEdges[i] = temp;
                    }
                }
            }

            _areaDimentions.x = _mapEdges[3].position.x - _mapEdges[1].position.x;
            _areaDimentions.y = _mapEdges[2].position.z - _mapEdges[0].position.z;

            var aspectRatio = _areaDimentions.x / _areaDimentions.y;

            var mapSize = new Vector2(_mapImage.sizeDelta.y * aspectRatio, _mapImage.sizeDelta.y);

            _mapImage.sizeDelta = mapSize;

            _mapDimentions = mapSize;

            var pivotX = Mathf.Abs(_mapEdges[1].position.x) / (Mathf.Abs(_mapEdges[1].position.x) + Mathf.Abs(_mapEdges[3].position.x));
            var pivotY = Mathf.Abs(_mapEdges[0].position.z) / (Mathf.Abs(_mapEdges[0].position.z) + Mathf.Abs(_mapEdges[2].position.z));

            _mapImage.pivot = new Vector2(pivotX, pivotY);
        }

        private void Update()
        {
            SetMapPosition();

            foreach (var enemyIcon in _enemies.Values)
            {
                SetMarkerPosition(enemyIcon);
            }
        }

        private void SetMapPosition()
        {
            var proportion = new Vector2(_playerReference.position.x / _areaDimentions.x, _playerReference.position.z / _areaDimentions.y);
            _mapImage.anchoredPosition = new Vector2(-proportion.x * _mapDimentions.x, -proportion.y * _mapDimentions.y);

            _playerIcon.rotation = Quaternion.Euler(new Vector3(0, 0, -_playerRotation.eulerAngles.y));
        }

        private void SetNewEnemy(Enemy enemy)
        {
            var icon = _enemyIconPool.Spawn(_playerIcon.parent);

            icon.color = enemy.Config.MapIconColor;
            SetMarkerPosition(enemy.transform, icon.rectTransform);

            icon.gameObject.SetActive(true);

            _enemies.Add(GetID(enemy), (enemy, icon));
        }

        private void UnlinkEnemy(Enemy enemy)
        {
            var icon = _enemies[GetID(enemy)].icon;

            _enemyIconPool.Unspawn(icon);

            _enemies.Remove(GetID(enemy));
        }

        private int GetID(Enemy enemy)
        {
            return enemy.GetInstanceID();
        }

        private void SetMarkerPosition(Transform go, RectTransform icon)
        {
            var proportion = new Vector2(go.position.x / _areaDimentions.x, go.position.z / _areaDimentions.y);
            icon.anchoredPosition = new Vector2(proportion.x * _mapDimentions.x + _mapImage.anchoredPosition.x,
                proportion.y * _mapDimentions.y + _mapImage.anchoredPosition.y);

            icon.rotation = Quaternion.Euler(new Vector3(0, 0, -go.eulerAngles.y));
        }

        private void SetMarkerPosition((Enemy enemy, Image icon) enemyIcon)
        {
            SetMarkerPosition(enemyIcon.enemy.transform, enemyIcon.icon.rectTransform);
        }
    }
}