using UnityEngine;

namespace Gameplay
{
    public class DoorsManager : MonoBehaviour
    {
        [SerializeField]
        private Door[] _doors;

        public void ResetLevel()
        {
            foreach (var door in _doors)
            {
                door.ResetLevel();
            }
        }
    }
}