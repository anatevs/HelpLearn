using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrewarmView : MonoBehaviour
    {
        public event Action OnClicked;

        [SerializeField]
        private Button _button;

        public void Show(bool isShow)
        {
            gameObject.SetActive(isShow);
        }

        public void SetInactive()
        {
            _button.interactable = false;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            OnClicked?.Invoke();
        }
    }
}