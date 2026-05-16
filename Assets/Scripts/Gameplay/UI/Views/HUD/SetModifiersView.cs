using System;
using UnityEngine;

namespace UI
{
    public sealed class SetModifiersView : MonoBehaviour,
        ISetModifiersView
    {
        public event Action OnApplyClicked;
        public event Action OnCancelClicked;

        [SerializeField]
        private ButtonView _applyView;

        [SerializeField]
        private ButtonView _cancelView;

        private void OnEnable()
        {
            _applyView.OnClicked += HandleApply;
            _cancelView.OnClicked += HandleCancel;
        }

        private void OnDisable()
        {
            _applyView.OnClicked -= HandleApply;
            _cancelView.OnClicked -= HandleCancel;
        }

        private void HandleApply()
        {
            OnApplyClicked?.Invoke();
        }

        private void HandleCancel()
        {
            OnCancelClicked?.Invoke();
        }
    }
}