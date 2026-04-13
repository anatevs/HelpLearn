using System;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "TurretConfig",
        menuName = "Configs/Weapons/Turret")]
    public class TurretConfig : BaseWeaponConfig<Turret>
    {
        public TurretParams TurretParams => _turretParams;

        public Color RadiusColor => _radiusColor;

        [SerializeField]
        private TurretParams _turretParams;

        [SerializeField]
        private Color _radiusColor;


        public override Turret CreateNewWeapon()
        {
            Turret turret = base.CreateNewWeapon();

            turret.Init(_weaponParams, _turretParams);

            return turret;
        }
    }


    [Serializable]
    public struct TurretParams
    {
        public float DetectionRadius => _detectionRadius;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField]
        private float _detectionRadius;

        [SerializeField]
        private float _rotationSpeed;
    }
}