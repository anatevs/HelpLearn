using UnityEngine;
using Gameplay;
using System.Linq;

namespace GameTest
{
    public class TargetSpawnAdjuster : MonoBehaviour
    {
        public bool IsTargetsOn => _isTargetsTestOn;

        public bool IsProjectilesOn => _isProjectilesTestOn;

        public int TargetsAmount => _targetsAmount;

        public int ProjectilesInitCount => _projectilesInitCount;

        [Header("Targets")]
        [SerializeField]
        private SpawnPointData _testSpawnData;

        [SerializeField]
        private TargetSpawnPoint _pointPrefab;

        [SerializeField]
        private float _pointsDistance;

        [SerializeField]
        private Transform _pointsParent;

        [SerializeField]
        private Transform _endPortal;

        [Header("Projectiles")]
        [SerializeField]
        private float _testProjectileSpeed;


        [Header("Test parameters")]
        [SerializeField]
        private bool _isTargetsTestOn = false;

        [SerializeField]
        private bool _isProjectilesTestOn = false;

        [SerializeField]
        private int _targetsAmount = 300;

        [SerializeField]
        private float _shootRate = 20f;

        private int _projectilesInitCount = 100;

        private float _startEndDistance = 40f;

        private float _bottomTargetsZ;

        public void SetupSpawn(TargetSpawnService targetSpawnService, Turret turret)
        {
            if (_isTargetsTestOn)
            {
                SetupTargetSpawn(targetSpawnService);
            }

            if (_isProjectilesTestOn)
            {
                SetupProjectilesSpawn(turret);
            }
        }

        public void SetupTargetSpawn(TargetSpawnService targetSpawnService)
        {
            DisableCurrentPoints();

            _startEndDistance = Mathf.Abs(_endPortal.position.x - _pointsParent.position.x);

            var pointsAmount = CalculatePointsAmount();

            _bottomTargetsZ = -(_pointsDistance * (pointsAmount - 1) / 2);

            for (int i = 0; i < pointsAmount; i++)
            {
                var zPosition = _bottomTargetsZ + _pointsDistance * i;

                var point = SpawnPoint(zPosition);

                point.Init(targetSpawnService);

                point.gameObject.SetActive(true);
            }

            var endPortalScale = Vector3.one;

            endPortalScale.z = pointsAmount;

            _endPortal.localScale = endPortalScale;
        }

        private void SetupProjectilesSpawn(Turret turret)
        {
            if (!_isTargetsTestOn)
            {
                SetDefaultTargetsBottomZ();
            }

            _projectilesInitCount = CalculateProjectileInitCount(turret.BarrelsAmount, turret.transform.position.z);

            turret.SetShootSpeedAndPeriod(_testProjectileSpeed, 1 / _shootRate);
        }

        private void DisableCurrentPoints()
        {
            var currentPoints = GetCurrentTargetPoints();

            foreach (var point in currentPoints)
            {
                point.gameObject.SetActive(false);
            }
        }

        private void SetDefaultTargetsBottomZ()
        {
            var currentPoints = GetCurrentTargetPoints();

            _bottomTargetsZ = currentPoints.Min(x => x.transform.position.z);
        }

        private TargetSpawnPoint[] GetCurrentTargetPoints()
        {
            return _pointsParent.gameObject.GetComponentsInChildren<TargetSpawnPoint>();
        }

        private TargetSpawnPoint SpawnPoint(float zPosition)
        {
            var point = Instantiate(_pointPrefab, _pointsParent);

            point.SetupData(_testSpawnData);

            var position = point.transform.localPosition;

            position.z = zPosition;

            point.transform.localPosition = position;

            point.gameObject.SetActive(false);

            return point;
        }

        private int CalculatePointsAmount()
        {
            var targetsDistance = _testSpawnData.SpawnPeriod * _testSpawnData.TargetSpeed;

            var rowAmount = (_startEndDistance - 2 * targetsDistance) / targetsDistance;

            return Mathf.CeilToInt(_targetsAmount / rowAmount);
        }

        private int CalculateProjectileInitCount(int barrelsAmount, float shootZ)
        {
            var shootTargetsDistance = Mathf.Abs(_bottomTargetsZ - shootZ);

            var oneBarrelAmount = (int)Mathf.Ceil(shootTargetsDistance * _shootRate / _testProjectileSpeed);

            return oneBarrelAmount * barrelsAmount;
        }
    }
}