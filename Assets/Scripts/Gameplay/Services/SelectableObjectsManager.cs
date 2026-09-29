using Assets.Input;
using GameManagement;
using UnityEngine;

namespace Gameplay
{
    public class SelectableObjectsManager : MonoBehaviour, IPausable
    {
        private InputHandler _input;

        private SelectableComponent _currentHovered = null;

        private bool _isPlaying = false;

        public void Init(InputHandler input)
        {
            _input = input;
        }

        private void OnEnable()
        {
            _input.OnSelectableHovered += HandleHover;
            _input.OnSelectableClicked += HandleClicked;
            _input.OnSelectableOut += ClearCurrent;
        }

        private void OnDisable()
        {
            _input.OnSelectableHovered -= HandleHover;
            _input.OnSelectableClicked -= HandleClicked;
            _input.OnSelectableOut -= ClearCurrent;
        }

        public void ResetLevel()
        {
            ClearCurrent();

            Resume();
        }

        public void Pause()
        {
            ClearCurrent();

            _isPlaying = false;
        }

        public void Resume()
        {
            _isPlaying = true;
        }

        private void HandleHover(GameObject go)
        {
            if (!_isPlaying)
            {
                return;
            }

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

        private void ClearCurrent()
        {
            if (_currentHovered != null)
            {
                _currentHovered.SetHovered(false);

                _currentHovered = null;
            }
        }
    }
}