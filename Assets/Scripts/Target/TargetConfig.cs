using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "TargetConfig",
        menuName = "Configs/Target")]
    public class TargetConfig : ScriptableObject
    {
        public Target Prefab => _prefab;

        [SerializeField]
        private Target _prefab;
    }
}