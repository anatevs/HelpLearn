using UnityEngine;

namespace Gameplay
{
    public sealed class TouchDamageView : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _renderer;

        private Color _activeColor;
        private Color _inactiveColor;

        public void Init(Color activeColor, Color inactiveColor)
        {
            _activeColor = activeColor;
            _inactiveColor = inactiveColor;
        }

        public void SetActive()
        {
            SetColor(_activeColor);
        }

        public void SetInactive()
        {
            SetColor(_inactiveColor);
        }
        public void SetRadius(float radius)
        {
            transform.localScale = new Vector3(radius, 1, radius);
        }

        private void SetColor(Color color)
        {
            _renderer.material.color = color;
        }
    }
}