using System;
using UnityEngine;

namespace Gameplay
{
    public class SelectableComponent : MonoBehaviour
    {
        public event Action OnClicked;

        public event Action<bool> OnHoverChanged;

        public void HandleClick()
        {
            OnClicked?.Invoke();
        }

        public void SetHovered(bool selected)
        {
            OnHoverChanged?.Invoke(selected);
        }
    }
}