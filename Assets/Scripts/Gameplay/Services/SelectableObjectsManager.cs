using Assets.Input;
using UnityEngine;

namespace Gameplay
{
    public class SelectableObjectsManager : MonoBehaviour
    {
        private SelectableComponent _currentHovered = null;

        [SerializeField]
        private InputHandler _input;

        private void OnEnable()
        {
            _input.OnSelectableHovered += HandleHover;
            _input.OnSelectableClicked += HandleClicked;
            _input.OnSelectableOut += HandleOut;
        }

        private void OnDisable()
        {
            _input.OnSelectableHovered -= HandleHover;
            _input.OnSelectableClicked -= HandleClicked;
            _input.OnSelectableOut -= HandleOut;
        }

        public void ResetLevel()
        {
            if (_currentHovered != null)
            {
                HandleOut();
            }
        }

        private void HandleHover(GameObject go)
        {
            if (go.TryGetComponent<SelectableComponent>(out var selectable))
            {
                if (_currentHovered != null)
                {
                    _currentHovered.SetHovered(false);
                }

                _currentHovered = selectable;
                _currentHovered.SetHovered(true);
            }
        }

        private void HandleClicked()
        {
            _currentHovered.HandleClick();
        }

        private void HandleOut()
        {
            _currentHovered.SetHovered(false);

            _currentHovered = null;
        }
    }
}