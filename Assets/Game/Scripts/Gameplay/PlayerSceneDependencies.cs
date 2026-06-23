using Assets.Input;
using UnityEngine;

namespace Gameplay
{
    public class PlayerSceneDependencies : MonoBehaviour
    {
        public CameraFollower CameraFollower => _cameraFollower;

        public InputHandler InputHandler => _inputHandler;

        [SerializeField]
        private CameraFollower _cameraFollower;

        [SerializeField]
        private InputHandler _inputHandler;
    }
}