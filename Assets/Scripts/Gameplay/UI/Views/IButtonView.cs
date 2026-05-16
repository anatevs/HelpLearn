using System;

namespace UI
{
    public interface IButtonView
    {
        public event Action OnClicked;
    }
}