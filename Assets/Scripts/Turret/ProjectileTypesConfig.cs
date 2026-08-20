using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ProjectileTypesConfig",
        menuName = "Configs/ProjectileTypes")]
    public class ProjectileTypesConfig : ScriptableObject
    {
        [SerializeField]
        private ProjectileTypeData[] _typeDatas = 
            Enum.GetValues(typeof(ProjectileType))
            .Cast<ProjectileType>()
            .Select(x => new ProjectileTypeData() { Type = x, SpeedMultiplier = 1})
            .ToArray();

        private Dictionary<ProjectileType, ProjectileTypeData> _data;

        public Dictionary<ProjectileType, ProjectileTypeData> GetData()
        {
            if (_data != null)
            {
                return _data;
            }

            _data = new();

            foreach (var data in _typeDatas)
            {
                _data.Add(data.Type, data);
            }

            return _data;
        }
    }

    [Serializable]
    public struct ProjectileTypeData
    {
        public ProjectileType Type;

        public float SpeedMultiplier;

        public Projectile Prefab;
    }
}