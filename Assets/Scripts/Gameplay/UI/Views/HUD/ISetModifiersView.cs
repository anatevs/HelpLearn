using System;

namespace UI
{
    public interface ISetModifiersView
    {
        public event Action OnApplyClicked;

        public event Action OnCancelClicked;
    }
}