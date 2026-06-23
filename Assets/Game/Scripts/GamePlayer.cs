using Gameplay;
using Mirror;
using UnityEngine;

namespace GameManagement
{
    public class GamePlayer : NetworkBehaviour
    {
        [SyncVar(hook = nameof(SetName))]
        public string Name = "nameDefault";

        [SyncVar(hook = nameof(SetColor))]
        public Color Color = Color.black;

        [SerializeField]
        private PlayerVisual _playerVisual;

        [SerializeField]
        private PlayerController _playerMoveController;

        [SerializeField]
        private PlayerRotation _playerRotation;

        [SerializeField]
        private Transform _cameraPoint;

        private CameraFollower _cameraFollower;

        public void Init(PlayerSceneDependencies sceneObjects)
        {
            _cameraFollower = sceneObjects.CameraFollower;
            _cameraFollower.SetPoint(_cameraPoint);

            _playerMoveController.Init(sceneObjects.InputHandler);
            _playerRotation.Init(sceneObjects.InputHandler);
        }

        private void Start()
        {
            _playerVisual.SetName(Name);

            _playerVisual.SetColor(Color);

            if (!isLocalPlayer)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log($"iskinematic for {Name}");
            }
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            _playerMoveController.UpdateMovement();
            //_playerRotation.UpdateRotation();
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            _playerMoveController.FixedUpdateMovement();
            _playerRotation.FixedUpdateRotation();
        }

        private void LateUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            _cameraFollower.Follow();
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            var sceneObjects = FindAnyObjectByType<PlayerSceneDependencies>();

            if (sceneObjects == null)
            {
                Debug.LogError("no PlayerSceneDependencies object on a scene");
            }

            Init(sceneObjects);
        }

        [Command]
        public void CmdSetName(string name)
        {

        }

        [Command]
        public void CmdSetColor(Color color)
        {

        }

        private void SetName(string oldName, string newName)
        {
            _playerVisual.SetName(newName);
        }

        private void SetColor(Color oldColor, Color newColor)
        {
            _playerVisual.SetColor(newColor);
        }
    }
}