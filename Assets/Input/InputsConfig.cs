using UnityEngine;

namespace Assets.Input
{
    [CreateAssetMenu(fileName = "InputsConfig",
        menuName = "Configs/Inputs")]
    public class InputsConfig : ScriptableObject
    {
        public InputHandler PCPrefab => _pcPrefab;
        public InputHandler MobilePrefab => _mobilePrefab;

        [SerializeField]
        private InputHandler _pcPrefab;

        [SerializeField]
        private InputHandler _mobilePrefab;
    }
}