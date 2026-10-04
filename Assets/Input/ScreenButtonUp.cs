using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Assets.Input
{
    [AddComponentMenu("Input/On-Screen Button Up")]
    public class OnScreenButtonUp : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData data)
        {
            SendValueToControl(0.0f);
        }

        public void OnPointerUp(PointerEventData data)
        {
            SendValueToControl(1.0f);
        }

        [InputControl(layout = "Button")]
        [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}